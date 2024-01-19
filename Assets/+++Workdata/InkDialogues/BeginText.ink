=== Level1 ===

= FirstText
I have to find Ava! What if something has happened to her? #speaker:Liam #layout:left

I should take a closer look around.Maybe I'll find some clues as to where she was... #speaker:Liam #layout:left

It's best to start looking in the village. #speaker:Liam #layout:left
->END

= Puzzle1Text
What's that? Huh? The pieces fit together... #speaker:Liam #layout:left
-> END

= Puzzle1Solved
A map? Could be useful. I'd better put it in my album... #speaker:Liam #layout:left
-> END

= KnockingDoor
... Looks like nobody is home. #speaker:Liam #layout:left
-> END

= CheckShop
Closed? Isn't it normally open at this time? #speaker:Liam #layout:left
-> END

= Oliver
EXTERNAL InfoOliver(sympathyCount)

Good day Liam! How are you? #speaker:Oliver #layout:right

~ InfoOliver(1)
Ah... Good day to you too, mayor. I'm well. #speaker:Liam #layout:left

That's good to hear. You seem a bit... mh nervous? Or a little worried? Is everything okay? #speaker:Oliver #layout:right

Oh, well... Yeah no, kind of. I'm looking for someone. Amelia, the owner of the shop. Do you know by any chance where she could be? #speaker:Liam #layout:left

At the shop? #speaker:Oliver #layout:right
+ [She wasn't there]
-> SheWasntThere
+ [I've just been there]
-> IJustBeenThere

= SheWasntThere
Hmm... How about that well in the forest? She's there pretty often. #speaker:Oliver #layout:right

Oh really? I'll have a look there then, thank you! #speaker:Liam #layout:left

... #speaker:Liam #layout:left

Just take this way #speaker:Oliver #layout:right

Thanks! #speaker:Liam #layout:left

(Thought): ... 'This way'- but where? I'll just take a look at the map. #speaker:Liam #layout:left
->END

= IJustBeenThere
Hmm... and you haven't seen her? #speaker:Oliver #layout:right

No haven't seen her. The shop was closed. #speaker:Liam #layout:left

Then maybe at the well in the forest? She's there pretty often. #speaker:Oliver #layout:right

Oh really? I'll be heading that way then, thank you! #speaker:Liam #layout:left

... #speaker:Liam #layout:left

Don't know the way? Just take this way. #speaker:Oliver #layout:right

Thanks! #speaker:Liam #layout:left

(Thought): ... 'This way'- but where? I'll just take a look at the map. #speaker:Liam #layout:left
-> END

= AmeliaWell
EXTERNAL WalkAway(animationTrigger, shopClosedDoorActive, shopOpenDoorActive)
EXTERNAL NPCWell(oliverActive, ameliaActive, transporterWithoutWoodActive, transporterWithWoodActive)
EXTERNAL InfoAmelia(sympathyCount)

Hey, Amelia, right? #speaker:Liam #layout:left

~ InfoAmelia(1)
Oh! You must be Liam, right? Ava told me about you! #speaker:Amelia #layout:right

Mmm...? Is everything okay? #speaker:Amelia #layout:right

Ava has disappeared and I found a polaroid of her that shows the shelf in the store, would it be possible to let me in? #speaker:Liam #layout:left

Oh? Don't worry too much, I'm sure she will be fine. #speaker:Amelia #layout:right

Could you open the shop for me please? Maybe I can find some clues about where she wanted to go. #speaker:Liam #layout:left

Sure! Wanted to reopen the shop in a minute, just follow me #speaker:Amelia #layout:right

~ WalkAway("Amelia_WalkOutForest", false, true)
~ NPCWell(false, false, false, true)
Thanks! #speaker:Liam #layout:left
-> END

= JackInWoods
EXTERNAL WalkToCabin(animationTrigger, jackActive)
EXTERNAL InfoJack(sympathyCount)

Hey! Where are you going? #speaker:Jack #layout:right

~InfoJack(1)
Mmm? Me?... Back to the shop. #speaker:Liam #layout:left

You'd better go home. #speaker:Jack #layout:right

Huh? Excuse me, but no. I need to find my wife. #speaker:Liam #layout:left

In the forest? #speaker:Jack #layout:right

+[Yes? Obviously.]
-> Obviously
+[Needed Amelia to open the shop for me.]
-> NeededAmelia

= Obviously
Get outta here! #speaker:Jack #layout:right

I was on my way already. #speaker:Liam #layout:left

Don't you dare to ever come back! #speaker:Jack #layout:right

~ WalkToCabin("Jack_WalkToCabin", false)
(Thought): What a weird guy. #speaker:Liam #layout:left
-> END

= NeededAmelia
Then you should go back, she just passed by here. #speaker:Jack #layout:right

Yes, was just on the way. #speaker:Liam #layout:left

You better don't come back. #speaker:Jack #layout:right

~WalkToCabin("Jack_WalkToCabin", false)
Huh...? #speaker:Liam #layout:left

(Thought): What an awkward guy. #speaker:Liam #layout:left
->END

= AmeliaShop1
Here we are, come on in! #speaker:Amelia #layout:right

Thanks, it won't take long. #speaker:Liam #layout:left

Look around and take all the time you need! #speaker:Amelia #layout:right
-> END

= AmeliaShopAfterPuzzle
Why do these mushrooms look so strange? #speaker:Liam #layout:left

Oh, you mean those? #speaker:Amelia #layout:right

+[Right, never saw mushrooms like this before.]
->NeverSawMushrooms
+[They don't look edible, do they?]
->NotEdible

= NeverSawMushrooms
Oh really? Not even in the forest? #speaker:Amelia #layout:right

Some of them slowly change color, you can see it if you look closely. #speaker:Amelia #layout:right

+[Change color?]
->ChangedColor
+[How is that possible?]
->HowPossible

= NotEdible
Haha, Yeah. They didn't always look like this. #speaker:Amelia #layout:right

They started changing color a while ago. #speaker:Amelia #layout:right

+[Changed color?]
->ChangedColor
+[How is that possible?]
->HowPossible

= ChangedColor
See, even some of the flowers in the Village started to change. #speaker:Amelia #layout:right

Do you know why? #speaker:Liam #layout:left

As far as I know, the others haven't changed anything about the care of the plants.It's kinda weird, but we don't know any other reason than the water. #speaker:Amelia #layout:right

Mmm... strange. #speaker:Liam #layout:left

(Thought): Maybe I should take a closer look at the flowers. #speaker:Liam #layout:left
-> END 

= HowPossible
Maybe because of the water in the lake, it is no longer drinkable. #speaker:Amelia #layout:right

It is not drinkable? But it looks clean? #speaker:Liam #layout:left

I'm not entirely sure, but we don't know any other reason than the water. #speaker:Amelia #layout:right

Mmm... strange. #speaker:Liam #layout:left

(Thought): Maybe I should take a closer look at this... #speaker:Liam #layout:left
->END

= Test
VAR player_name = ""

Hello
What is your name?
+ [My name is Bob!]
    ~ player_name = "Bob"
    -> name_choosen
+ [My name is Bobby!]
    ~ player_name = "Bobby"
    -> name_choosen
    
=== name_choosen ===    
hello, {player_name}!
Nice to meet you.
-> main_dialogue_loop

=== main_dialogue_loop ===

+ [Tell me about this place!]
    Sure! What do you want to know?
    -> info_about_place

+ [I have nothing more to talk about!]
    Okay, bye.
    -> END

=== info_about_place ===
* [Tell me about the giant oak!]
    I dont know anything about that!
    -> info_about_place
* [Tell me about the path south!]
    I dont know anything about that!
    -> info_about_place

+ [Let`s talk about something else.]
    -> main_dialogue_loop

-> END