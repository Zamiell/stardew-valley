# Setting a Manual Seed

* The game seed / uniqueIDForThisGame  corresponds to the Epoch timestamp that the title screen was entered. (Either from the desktop or from in-game.)
* To convert a seed to the epoch timestamp, you need to add 1340323200.
* To start a new game with a specific seed:
  1) Perform this conversion to get the desired Epoch timestamp.
  2) Convert the Epoch timestamp to a human readable date. <https://www.epochconverter.com/>
  3) Set your local system clock to be 1 minute prior to desired date.
  4) Open Stardew and go into an existing game.
  5) Change the options to windowed mode.
  6) Open up the clock by clicking on the bottom right hand corner of the desktop (in Windows, at least).
  7) Exit to the title screen at the exact second.
  8) Create a new game, and it should have the desired seed.
* Note that some things in the game are not affected by the seed, such as the amount of Spring Onions.
