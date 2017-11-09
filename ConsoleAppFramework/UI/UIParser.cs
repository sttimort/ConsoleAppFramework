using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.UI
{
    public class UIParser : IUIParser
    {
        string pattern;
        IDictionary<string, CommandInfo> commandInfoByID;

        public UIParser(IDictionary<string, CommandInfo> commandInfoByID)
        {
            this.commandInfoByID = commandInfoByID;
            pattern = generatePattern();
        }


        public string ParseInput(string input, out string args)
        {
            args = null;
            Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

            foreach (var ID in commandInfoByID.Keys)
            {
				if (m.Groups["g"+ID].Success)
                {
                    args = m.Groups["args"].Value;
                    return ID;
                }
            }
            return null;
        }

        string generatePattern()
        {
            var sb = new StringBuilder(@"^\s*(?:");
            foreach (var entry in commandInfoByID)
            {
                var ID = entry.Key;
                var cmdInfo = entry.Value;

                sb.Append($"(?<g{ID}>{Regex.Escape(cmdInfo.Name)}|");

                var escapedAliases = new List<string>();
                Array.ForEach(cmdInfo.Aliases ?? new string[0], (str) =>
                              escapedAliases.Add(Regex.Escape(str)));
                
                sb.Append(string.Join("|", escapedAliases));
                sb.Append(")|");
            }
            sb.Remove(sb.Length - 1, 1); // remove redundant bar
            sb.Append(@")(?<args>(?:\s+.*)?)$");

            return sb.ToString();
        }
    }
}
