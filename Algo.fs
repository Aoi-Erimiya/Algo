(*
  Algo.fs
  Copyright (c) 2019 Hiroaki Wada
  This software is released under the MIT License.
  http://opensource.org/licenses/mit-license.php
*)
open System

type Math private() =
    static let rand = Random();
    static member Next(arg : int) : int =
        rand.Next(arg)

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
    Console.WriteLine("")

let algoComparator cards: List<AlgoCard> = 
    cards |> List.sortWith(fun x y -> 
        match x.Number < y.Number, x.Number = y.Number, x.Color = Color.Black with
        | true, true, true -> -1
        | true, true, false -> -1
        | true, false, false -> -1
        | true, false, true -> -1
        | false, true, true -> -1
        | false, true, false -> 1
        | false, false, true -> 1
        | false, false, false -> 1
    )

let shuffle cards = 
    cards |> List.sortBy(fun _ -> Guid.NewGuid())

[<EntryPoint>]
let main argv = 
    let mutable cards = []
    for i in 0..11 do
        cards <- List.append [AlgoCard(i, Color.Black)] cards
        cards <- List.append [AlgoCard(i, Color.White)] cards

    let shuffledCards = cards |> shuffle
    shuffledCards |> showCards

    // スライス的な何か
    //cards.[5*i..5*(i+1)-1]
    
    // sort用に同数なら白を強い扱いにするコンパレーターを作る
    cards |> algoComparator |> showCards
    
    // 残からランダムドローして、アタック or 下がるか決める
    // アタックの場合は、何枚目を指定するか決める。（番号だと誤認するので、アルファベット降った方がよい？）
    // 成功したらオープンにする。そしてもっかいアタックするか決める。
    // 失敗したら、引いたカードを自分の手札に。

    Console.ReadLine() |> ignore

    0 // return an integer exit code