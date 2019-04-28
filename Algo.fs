(*
Algo.fs

Copyright (c) 2019 Hiroaki Wada

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*)
open System

type Color =
    | Black = 0
    | White = 1

type AlgoCard(number:int, color:Color) = 
    let color:Color = color
    let number:int = number

    member x.Color with get() = color
    member x.Number with get() = number

    override x.ToString() = 
        if x.Color = Color.Black then 
            Console.ForegroundColor <- ConsoleColor.White
            Console.BackgroundColor <- ConsoleColor.Black
        else
            Console.ForegroundColor <- ConsoleColor.Black
            Console.BackgroundColor <- ConsoleColor.White
        
        x.Number.ToString()

let showCards cards =
    cards |> List.iter(fun x ->
        Console.Write(x.ToString())
        Console.ResetColor()
        Console.Write(" ")
        )

[<EntryPoint>]
let main argv = 
    let mutable cards = []
    for i in 0..11 do
        cards <- List.append [AlgoCard(i, Color.Black)] cards
        cards <- List.append [AlgoCard(i, Color.White)] cards

    cards |> List.sortBy(fun x -> x.Number + int x.Color) |> showCards

    // cards |> showCards
    
    // sort用に同数なら白を強い扱いにするコンピレーターを作る

    // 残からランダムドローして、アタック or 下がるか決める
    // アタックの場合は、何枚目を指定するか決める。（番号だと誤認するので、アルファベット降った方がよい？）
    // 成功したらオープンにする。そしてもっかいアタックするか決める。
    // 失敗したら、引いたカードを自分の手札に。

    Console.ReadLine() |> ignore

    0 // return an integer exit code
