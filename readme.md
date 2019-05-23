# Building

* Download and extract `pivotal-gemfire-9.5.1` and `pivotal-gemfire-native-9.2.2` into the `lib` directory
* Install .net core from eg. https://dotnet.microsoft.com/download/linux-package-manager/ubuntu18-04/sdk-current
* Restore the project config (that's excluded from git)
```
cd client
dotnet restore
```

# Running the client

```
cd client
dotnet run
```
