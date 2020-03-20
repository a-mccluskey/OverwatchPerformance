# OverwatchPerformance
In the Templates folder there are the Open office versions of the app I'm trying to create

Going to do this in a few steps:
* First create a console app
* Use the existing classes to create a UI
* Expand UI to a web interface

# Console App
Needs a few core classes, the game data, the progam / UI, data saving & reading (ititally a CSV file but move to a DB approach), A config file for which maps are available this season.
The game data class needs to hold: SR, Map, List of heroes, total healing, game length, deaths and DateTime of the game being played. The rest of the stats can be generated on the fly.
There will need to be one inital piece of data - the inital SR from when we start tracking.


