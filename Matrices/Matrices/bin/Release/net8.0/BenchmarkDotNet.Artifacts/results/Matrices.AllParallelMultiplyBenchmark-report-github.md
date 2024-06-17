```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3593/23H2/2023Update/SunValley3)
Intel Xeon CPU E5-2630 0 2.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.205
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX [AttachedDebugger]
  Job-IEMLEH : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX

IterationCount=3  WarmupCount=5  

```
| Method                 | Size | Mean         | Error        | StdDev     | Ratio | Completed Work Items | Lock Contentions |
|----------------------- |----- |-------------:|-------------:|-----------:|------:|---------------------:|-----------------:|
| **Multiply**               | **250**  |     **94.94 ms** |    **12.049 ms** |   **0.660 ms** |  **1.00** |                    **-** |                **-** |
| MultiplyParallel       | 250  |     22.24 ms |     3.624 ms |   0.199 ms |  0.23 |              12.8438 |                - |
| MultiplyParallelBanded | 250  |     26.66 ms |     6.508 ms |   0.357 ms |  0.28 |             477.2813 |           0.1250 |
| MultiplyParallelTiled  | 250  |     23.49 ms |     6.749 ms |   0.370 ms |  0.25 |              12.8125 |                - |
|                        |      |              |              |            |       |                      |                  |
| **Multiply**               | **500**  |    **871.96 ms** |    **99.482 ms** |   **5.453 ms** |  **1.00** |                    **-** |                **-** |
| MultiplyParallel       | 500  |    198.45 ms |    49.019 ms |   2.687 ms |  0.23 |              15.0000 |                - |
| MultiplyParallelBanded | 500  |    218.75 ms |    20.614 ms |   1.130 ms |  0.25 |            1097.0000 |                - |
| MultiplyParallelTiled  | 500  |    186.85 ms |    32.256 ms |   1.768 ms |  0.21 |              15.0000 |                - |
|                        |      |              |              |            |       |                      |                  |
| **Multiply**               | **1000** |  **9,540.45 ms** | **1,926.392 ms** | **105.592 ms** |  **1.00** |                    **-** |                **-** |
| MultiplyParallel       | 1000 |  1,957.96 ms |   478.596 ms |  26.233 ms |  0.21 |              56.0000 |                - |
| MultiplyParallelBanded | 1000 |  2,048.95 ms |   283.849 ms |  15.559 ms |  0.21 |            1835.0000 |           2.0000 |
| MultiplyParallelTiled  | 1000 |  1,527.80 ms |   654.948 ms |  35.900 ms |  0.16 |              56.0000 |           1.0000 |
|                        |      |              |              |            |       |                      |                  |
| **Multiply**               | **1500** | **38,815.16 ms** | **3,306.047 ms** | **181.216 ms** |  **1.00** |                    **-** |                **-** |
| MultiplyParallel       | 1500 |  7,407.20 ms | 1,499.736 ms |  82.206 ms |  0.19 |             125.0000 |                - |
| MultiplyParallelBanded | 1500 |  7,570.02 ms |   949.014 ms |  52.019 ms |  0.20 |            2012.0000 |           1.0000 |
| MultiplyParallelTiled  | 1500 |  5,036.84 ms | 1,219.412 ms |  66.840 ms |  0.13 |             183.0000 |                - |
|                        |      |              |              |            |       |                      |                  |
| **Multiply**               | **2000** | **83,858.15 ms** |   **495.523 ms** |  **27.161 ms** |  **1.00** |                    **-** |                **-** |
| MultiplyParallel       | 2000 | 18,990.26 ms | 2,128.249 ms | 116.657 ms |  0.23 |             167.0000 |                - |
| MultiplyParallelBanded | 2000 | 19,516.97 ms | 5,472.329 ms | 299.957 ms |  0.23 |            2458.0000 |           1.0000 |
| MultiplyParallelTiled  | 2000 | 12,434.89 ms |   186.562 ms |  10.226 ms |  0.15 |             551.0000 |                - |
