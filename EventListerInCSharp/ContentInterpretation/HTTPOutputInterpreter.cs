﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EventListerInCSharp
{
    public class HTTPOutputInterpreter : IContentInterpreter
    {
        private List<string> _cachedOutput = null;

        public virtual IEnumerable<string> Interpret(string content)
        {
            if (_cachedOutput != null)
            {
                return _cachedOutput;
            }

            List<string> entries = new List<string>();

            var regex = new Regex(
                "<div\\ class=\"views-row.*?<div.*?class=\"title\".*?<a\\ href.*?\">\\W*(.*?)\\W*<\\/a>.*?<a class=\"more-link\".*?<\\/div>", 
                RegexOptions.Singleline);

            var matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                var entry = match.Groups[1].Value;
                entries.Add(entry);
            }

            return _cachedOutput = entries;
        }
    }
}
