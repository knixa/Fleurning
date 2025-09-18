
open System.IO
open ComputationExpressions.AsyncDemo

Path.Combine(__SOURCE_DIRECTORY__, "customers.csv")
|> getFileInformation
|> Async.RunSynchronously
|> printf "%A"