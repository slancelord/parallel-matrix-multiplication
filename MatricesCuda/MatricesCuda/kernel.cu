#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <stdio.h>
#include <iostream>
#include <random>
#include <chrono>
#include <iomanip>

#define TILE_SIZE 32

__global__ void matrixMultiplyKernel(int* a, int* b, int* c, int m, int n, int k) {
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;

	if (row < m && col < k) {
		int sum = 0;
		for (int i = 0; i < n; i++) {
			sum += a[row * n + i] * b[i * k + col];
		}
		c[row * k + col] = sum;
	}
}

__global__ void matrixMultiplySharedKernel(int* a, int* b, int* c, int m, int n, int k) {
	
	__shared__ int sharedA[TILE_SIZE][TILE_SIZE];
	__shared__ int sharedB[TILE_SIZE][TILE_SIZE];

	
	int row = blockIdx.y * TILE_SIZE + threadIdx.y;
	int col = blockIdx.x * TILE_SIZE + threadIdx.x;

	int sum = 0;

	
	for (int phase = 0; phase < (n - 1) / TILE_SIZE + 1; ++phase) {
		
		if (row < m && phase * TILE_SIZE + threadIdx.x < n) {
			sharedA[threadIdx.y][threadIdx.x] = a[row * n + phase * TILE_SIZE + threadIdx.x];
		}
		else {
			sharedA[threadIdx.y][threadIdx.x] = 0;
		}

		if (col < k && phase * TILE_SIZE + threadIdx.y < n) {
			sharedB[threadIdx.y][threadIdx.x] = b[(phase * TILE_SIZE + threadIdx.y) * k + col];
		}
		else {
			sharedB[threadIdx.y][threadIdx.x] = 0;
		}

		__syncthreads();

		
		for (int i = 0; i < TILE_SIZE; ++i) {
			sum += sharedA[threadIdx.y][i] * sharedB[i][threadIdx.x];
		}

		__syncthreads();
	}

	
	if (row < m && col < k) {
		c[row * k + col] = sum;
	}
}


void allocateAndCopyToDevice(int** devicePtr, int* hostPtr, int size) {
	cudaMalloc(devicePtr, size * sizeof(int));
	cudaMemcpy(*devicePtr, hostPtr, size * sizeof(int), cudaMemcpyHostToDevice);
}


void freeDeviceMemory(int* devicePtr) {
	cudaFree(devicePtr);
}

void printMatrix(int* matrix, int rows, int cols) {
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			std::cout << std::setw(4) << matrix[i * cols + j];
		}
		std::cout << std::endl;
	}
}

int main() {
	const int m = 15000, n = 15000, k = 15000;
	int* a = new int[m * n];
	int* b = new int[n * k];
	int* c = new int[m * k];


	std::random_device rd;
	std::mt19937 gen(rd());
	std::uniform_int_distribution<> dis(0, 9);

	for (int i = 0; i < m * n; i++) {
		a[i] = dis(gen);
	}

	for (int i = 0; i < n * k; i++) {
		b[i] = dis(gen);
	}

	int* d_a, * d_b, * d_c;


	allocateAndCopyToDevice(&d_a, a, m * n);
	allocateAndCopyToDevice(&d_b, b, n * k);
	allocateAndCopyToDevice(&d_c, c, m * k);


	dim3 threadsPerBlock(TILE_SIZE, TILE_SIZE);
	dim3 numBlocks((k + threadsPerBlock.x - 1) / threadsPerBlock.x, (m + threadsPerBlock.y - 1) / threadsPerBlock.y);


	auto start = std::chrono::high_resolution_clock::now();
	
	//matrixMultiplyKernel << <numBlocks, threadsPerBlock >> > (d_a, d_b, d_c, m, n, k);
	matrixMultiplySharedKernel << <numBlocks, threadsPerBlock >> > (d_a, d_b, d_c, m, n, k);
	
	cudaDeviceSynchronize();
	auto end = std::chrono::high_resolution_clock::now();


	std::chrono::duration<double> elapsed = end - start;
	std::cout << "Time: " << elapsed.count() << " sec" << std::endl;


	//std::cout << "Matrix A:" << std::endl;
	//printMatrix(a, m, n);

	//std::cout << "Matrix B:" << std::endl;
	//printMatrix(b, n, k);


	//cudaMemcpy(c, d_c, m * k * sizeof(int), cudaMemcpyDeviceToHost);

	//std::cout << "Matrix C:" << std::endl;
	//printMatrix(c, m, k);


	freeDeviceMemory(d_a);
	freeDeviceMemory(d_b);
	freeDeviceMemory(d_c);
	delete[] a;
	delete[] b;
	delete[] c;


	return 0;
}