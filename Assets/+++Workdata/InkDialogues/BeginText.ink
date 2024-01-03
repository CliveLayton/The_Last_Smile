=== Level1 ===

= FirstText
I need to go to the village, there must be a hint to find Ava.
->END

= Puzzle1Text
What is this? Looks a bit like a map. Maybe I can put them together and use it to orient myself
-> END

= Puzzle1Solved
Now I can orient myself in this forest, I should stick it in my album.
-> END

= KnockingDoor
... hmm seems no one is in there.
-> END

= CheckShop
Oh it’s closed.
-> END

= Ethan
Ethan: “Good day Liam! How are you?”

Liam: “Ah... Good day to you too, mayor. I'm well”

Ethan: “That's good to hear. You seem a bit... mh nervous? or frantic though? is really everything okay?”
+ [Oh, well...]
-> ContinueDialogue

=== ContinueDialogue ===
Liam: “Oh, well.... Yeah no, kind of. I'm looking for someone. Amelia, the owner of the Shop. Is there a chance you know where she could be?”

Ethan: “At their shop?”

Liam: “She wasn't there”

Ethan: “Hmm… then maybe at the well in the forest? She's there pretty often.”
+ [Oh, really?]
-> EndDialogue
+ [Where is the well?]
-> Answer

=== Answer ===
Ethan: “Just go this way” 

Liam: “Thanks!”

Liam(Thought): “... ‘This way’ where? I will just look at the map”
-> END

=== EndDialogue ===
Liam: “Oh really? I'll look there then, thank you!”
-> END


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