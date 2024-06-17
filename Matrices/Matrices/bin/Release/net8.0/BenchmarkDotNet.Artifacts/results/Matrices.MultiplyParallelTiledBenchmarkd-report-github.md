```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3593/23H2/2023Update/SunValley3)
Intel Xeon CPU E5-2630 0 2.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.205
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX [AttachedDebugger]
  Job-SEQHDO : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX

IterationCount=3  WarmupCount=5  

```
| Method                | Size | Mean         | Error      | StdDev    | Completed Work Items | Lock Contentions |
|---------------------- |----- |-------------:|-----------:|----------:|---------------------:|-----------------:|
| **MultiplyParallelTiled** | **250**  |     **21.85 ms** |   **1.996 ms** |  **0.109 ms** |              **12.8438** |                **-** |
| **MultiplyParallelTiled** | **500**  |    **173.57 ms** |  **57.782 ms** |  **3.167 ms** |              **13.7500** |                **-** |
| **MultiplyParallelTiled** | **1000** |  **1,708.36 ms** | **186.806 ms** | **10.239 ms** |              **95.0000** |           **1.0000** |
| **MultiplyParallelTiled** | **1500** |  **4,992.03 ms** | **391.737 ms** | **21.472 ms** |             **278.0000** |                **-** |
| **MultiplyParallelTiled** | **2000** | **14,348.57 ms** | **423.849 ms** | **23.233 ms** |             **412.0000** |                **-** |
