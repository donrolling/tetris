module Input

open System

let rec getNumericInputWithinRange(msg:string, errMsg:string, range:int list) : int = 
   let handleError(msg:string, errMsg:string, range:int list) = 
      Console.Clear()
      printfn "%s must be in range: %A" errMsg range

   printfn "%s" msg
   let input = Console.ReadLine()
   let result = 
      match Int32.TryParse input with
         | (true, result) -> Some(result) 
         | (false, _) -> None
   if result.IsSome then
      if range |> List.exists ((=) result.Value) then
         result.Value
      else
         handleError(msg, errMsg, range)
         getNumericInputWithinRange(msg, errMsg, range)
   else
      handleError(msg, errMsg, range)
      getNumericInputWithinRange(msg, errMsg, range)

let rec getNumericInput(msg:string, errMsg:string) : int = 
   let handleError(msg:string, errMsg:string) = 
      Console.Clear()
      printfn "%s" errMsg

   printfn "%s" msg
   let input = Console.ReadLine()
   let result = 
      match Int32.TryParse input with
         | (true, result) -> Some(result) 
         | (false, _) -> None
   if result.IsSome then
      result.Value
   else
      handleError(msg, errMsg)
      getNumericInput(msg, errMsg)
