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