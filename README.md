## Spell checker demo
This is a very naive implementation of a Kurdish spell checker. It serves as a proof of concept only. There are many issues which need to be addressed before it can be useful:
 - Currently it calculates the lavenshtein distance of every word in the dictionary every time it checks the spelling of a word.
 - Because of the above point, it's too slow to be practical.
 - It doesn't deal with Kurdish Clitic (sticky) pronouns.