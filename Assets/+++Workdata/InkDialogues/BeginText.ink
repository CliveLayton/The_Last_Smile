=== Level1 ===

= FirstText
I need to go to the village, there must be a hint to find Ava. #speaker:Liam #layout:left
->END

= Puzzle1Text
What is this? Looks a bit like a map. Maybe I can put them together and use it to orient myself #speaker:Liam #layout:left
-> END

= Puzzle1Solved
Now I can orient myself in this forest, I should stick it in my album. #speaker:Liam #layout:left
-> END

= KnockingDoor
... hmm seems no one is in there. #speaker:Liam #layout:left
-> END

= CheckShop
Oh it’s closed. #speaker:Liam #layout:left
-> END

= Oliver
Good day Liam! How are you? #speaker:Oliver #layout:right

Ah... Good day to you too, mayor. I'm well #speaker:Liam #layout:left

That's good to hear. You seem a bit... mh nervous? or frantic though? is really everything okay? #speaker:Oliver #layout:right
+ [Oh, well...]
-> ContinueDialogue

= ContinueDialogue
Oh, well.... Yeah no, kind of. I'm looking for someone. Amelia, the owner of the Shop. Is there a chance you know where she could be? #speaker:Liam #layout:left

At their shop? #speaker:Oliver #layout:right

She wasn't there #speaker:Liam #layout:left

Hmm… then maybe at the well in the forest? She's there pretty often. #speaker:Oliver #layout:right
+ [Oh, really?]
-> EndDialogue
+ [Where is the well?]
-> Answer

= Answer 
Just go this way #speaker:Oliver #layout:right

Thanks! #speaker:Liam #layout:left

(in thoughts) ... ‘This way’ where? I will just look at the map #speaker:Liam #layout:left
-> END

= EndDialogue
Oh really? I'll look there then, thank you! #speaker:Liam #layout:left
-> END

= AmeliaWell
EXTERNAL WalkAway(animationTrigger, shopClosedDoorActive, shopOpenDoorActive, oliverActive)

Hi Liam. #speaker:Amelia #layout:right

Hi Amelia. #speaker:Liam #layout:left

I will open the shop, no problem. #speaker:Amelia #layout:right

Allright, thank you. #speaker:Liam #layout:left

~ WalkAway("Amelia_WalkOutForest", false, true, false)
See you. #speaker:Amelia #layout:right
-> END

= JackInWoods
EXTERNAL WalkToCabin(animationTrigger)
What are you doing here?!! #speaker:Jack #layout:right

Sorry bro. #speaker:Liam #layout:left

~ WalkToCabin("Jack_WalkToCabin")
Get the fuck out of here. #speaker:Jack #layout:right
-> END

= AmeliaShop1
Here you go, you can find the bottles in the regal over there. #speaker:Amelia #layout:right

Thank you Amelia. #speaker:Liam #layout:left

Good luck. #speaker:Amelia #layout:right
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