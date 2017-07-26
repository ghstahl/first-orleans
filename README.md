# first-orleans
My First Microsoft Orleans App

## Azure
Make sure you get the latest [azure storage emulator](https://azure.microsoft.com/en-us/downloads/)

1. [Console Host](src/HelloSiloHost)  This is a simple host, or
2. [Azure Host](src/OrleansAzureSample) This is the same thing hosted on Azure
3. [Console Client](src/ConsoleApp1)  This is the client that calls into the grains

## Orleans 2.0 .Net Core
[Orleans 2.0 .Net Core](https://blogs.msdn.microsoft.com/orleans/2017/03/02/orleans-1-4-and-2-0-tech-preview-2-for-net-core-released/)
1. [Console host](src/SiloHost.Core) This hosts the orlean grains
2. [Console Client](src/OrleansClient.Core)  This is the client that calls into the grains


## OrleansHttp
```
This is hosted in the OrleansAzureSample
http://localhost:12399/grain/IHello/0/SayHello/?greeting=%22Good%20Bye%22
```
