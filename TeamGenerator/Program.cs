using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TeamGenerator
{
    class Program
    {
        class Team
        {
            public List<string> players = new List<string>();
            public string teamName;

            public override string ToString()
            {
                string output = string.Format("{0,-30}:{1}", teamName, FormatTeamNames());

                return output;
            }

            public string FormatTeamNames()
            {
                int width = (players.Count + 1).ToString("d").Length;

                string formatString = "\t{0, 25},";

                List<string> strParts = new List<string>();

                for (int i = 0; i < players.Count; i++)
                {
                    strParts.Add(string.Format(formatString, players[i]));
                }

                string resultStr = "";

                for (int i = 0; i < strParts.Count; i++)
                {
                    resultStr += strParts[i];
                }

                return resultStr;
            }
        }

        static Random rand = new Random();
        static void Main(string[] args)
        {
            int teamsize = 2;

            string peopleFile = "people.txt";

            List<string> argsList = args.ToList<string>();

            if (argsList.Contains("-c"))
            {
                int index = argsList.IndexOf("-c") + 1;

                bool parseSucc = Int32.TryParse(argsList[index], out teamsize);

                if (!parseSucc)
                    teamsize = 2;
            }
            if (argsList.Contains("-p"))
            {
                int index = argsList.IndexOf("-p") + 1;

                peopleFile = argsList[index];
            }


            try
            {
                using (FileStream fStream = new FileStream(peopleFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fStream))
                    {
                        List<string> names = new List<string>();
                        List<Team> teams = new List<Team>();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            names.Add(line);
                        }

                        names = Shuffle(names);

                        string teamsFile = string.Format("teams-{0}.txt", teamsize);

                        using (FileStream oFStream = new FileStream(teamsFile, FileMode.Create, FileAccess.ReadWrite))
                        {
                            using (StreamWriter writer = new StreamWriter(oFStream))
                            {
                                int lastIndex = 0;
                                for (int i = 0; i < names.Count - (teamsize - 1); i += teamsize)
                                {
                                    Team tempTeam = new Team();

                                    for (int j = i; j < i + teamsize; j++)
                                    {
                                        tempTeam.players.Add(names[j]);
                                    }

                                    tempTeam.teamName = string.Format("Team {0}", names[i]);
#if DEBUG
                                    Console.WriteLine(tempTeam.ToString());                                
#endif

                                    writer.WriteLine(tempTeam.ToString());

                                    lastIndex = i;
                                }

                                Team lastTeam = new Team();

                                for (int i = lastIndex + teamsize; i < names.Count; i++)
                                {
                                    
                                    lastTeam.players.Add(names[i]);
                                    

                                    lastTeam.teamName = string.Format("Team {0}", names[lastIndex + teamsize]);

                                }

                                if (lastTeam.players.Count > 0)
                                {
#if DEBUG
                                    Console.WriteLine(lastTeam.ToString());                                
#endif

                                    writer.WriteLine(lastTeam.ToString());
                                }

#if !DEBUG
                                Console.WriteLine("Teams saved to " + teamsFile);
#endif
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The specified people file was not found.");
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
        }

        private static List<string> Shuffle(List<string> sequence)
        {
            string[] retArray = sequence.ToArray();


            for (int i = 0; i < retArray.Length - 1; i += 1)
            {
                int swapIndex = rand.Next(i + 1, retArray.Length);
                string temp = retArray[i];
                retArray[i] = retArray[swapIndex];
                retArray[swapIndex] = temp;
            }

            return retArray.ToList<string>();
        }
    }
}
