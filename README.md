## Team Generator Readme ##

# Overview #

Allows individuals to generate random team matchups for tournaments.  Teams generated
will all have the same number of players (N x N matchups).

# Usage #
To run this program from the command line:

1. Open your command prompt in Windows.
2. Type cd [insert directory where TeamGenerator.exe is located here]
3. Type TeamGenerator.exe
    1. Optional: Type -p [player-file] to specify the file with all players.  Defaults to "players.txt"
	2. Optional: Type -c [count] to specify players on each team.  Defaults to 2.
4. Hit "Enter."  The program will tell you where you can find the generated teams.

# TODO #

- Add more shuffling algorithms, to give users some variety.
- Make the application more robust.
- Make it more user friendly.

# System Requirements #

- Requires Windows XP or later.
- Requires VS2010 to build
- Requires .NET 3.5 Client Profile to build and run