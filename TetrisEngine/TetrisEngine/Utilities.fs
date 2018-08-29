namespace TetrisEngine

module Utilities = 
   open System.Timers
   
   let SetupTimer(timerInterval : float, handler) =
      let Timer = new System.Timers.Timer()
      Timer.Elapsed.Add(handler)
      Timer.Interval <- timerInterval
      Timer.Enabled <- true
      Timer

   let EmptyTimer() = 
      let Timer = new System.Timers.Timer()
      Timer.Enabled <- false
      Timer
