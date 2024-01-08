using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AES
{
    public class Tokeniser
    {
        public Dictionary<string, int> variables = new Dictionary<string, int>(); 
        private Dictionary<string, object> tokeniseOperand(string input)
        {
            Dictionary<string, object> operandToken = new Dictionary<string, object>();
            if (int.TryParse(input, out int value))
            {
                operandToken.Add("type", "integer");
                operandToken.Add("value", value);
            }
            else if ("+-/*,".Contains(input))
            {
                operandToken.Add("type", "operator");
                operandToken.Add("value", input);
            }
            else
            {
                operandToken.Add("type", "variable");
                operandToken.Add("value", input);
            }
            return operandToken;
        }
        public Dictionary<string, object> getTokens(string line)
        {
            // Voodoo magic, hand crafted regex to return groups for commands with and without parameters and operators for variables and integers
            Regex cmdPattern = new Regex("([A-Za-z]+)\\(([A-Za-z0-9]*)(( ?([+-/*]) ?([A-Za-z0-9]+)))*(, ?([A-Za-z0-9]+)(( ?([+-/*]) ?([A-Za-z0-9]+)))*)*\\)");
            Regex assignPattern = new Regex("var ([A-Za-z0-9]+) ?= ?([A-Za-z0-9]+( ?([+-/*]) ?([A-Za-z0-9]+))*)");

            Dictionary<string, object> token = new Dictionary<string, object>();

            if (assignPattern.IsMatch(line))
            {
                Match matchedPattern = assignPattern.Match(line);
                token.Add("type", "assignment");
                token.Add("name", matchedPattern.Groups[1].Value);
                List<object> operands = new List<object>();

                if (matchedPattern.Groups[2].Value != null)
                    operands.Add(tokeniseOperand(matchedPattern.Groups[2].Value));

                for (int i = 0; i < matchedPattern.Groups[4].Captures.Count(); i++)
                {
                    operands.Add(tokeniseOperand(matchedPattern.Groups[4].Captures[i].Value));
                    operands.Add(tokeniseOperand(matchedPattern.Groups[5].Captures[i].Value));
                }
                token.Add("parameters", operands);
            }
            else if (cmdPattern.IsMatch(line))
            {
                Match matchedPattern = cmdPattern.Match(line);
                string command = matchedPattern.Groups[1].Value;
                token.Add("type", "function");
                token.Add("name", command);
                List<object> operands = new List<object>();
                if (matchedPattern.Groups[2].Value != null) //First parameter
                    operands.Add(tokeniseOperand(matchedPattern.Groups[2].Value));

                for (int i = 0; i < matchedPattern.Groups[5].Captures.Count(); i++) //Other parameters
                {
                    operands.Add(tokeniseOperand(matchedPattern.Groups[5].Captures[i].Value));
                    operands.Add(tokeniseOperand(matchedPattern.Groups[6].Captures[i].Value));
                }
                token.Add("parameters", operands);
            }

            return token;
        }
    }
}
