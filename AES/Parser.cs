using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    public class Parser
    {
        public Dictionary<string, int> variables = new Dictionary<string, int>();
        public Tokeniser tokeniser = new Tokeniser();
        public void parseLine(string line)
        {
            Dictionary<string, object> token = tokeniser.getTokens(line);
            if (!token.ContainsKey("type") || token["type"] == null)
                throw new Exception("Parser could not find token type");

            string type = (string)token["type"];

            switch (type)
            {
                case "assignment":
                    assignmentHandler(token);
                    break;
                case "function":
                    functionHandler(token);
                    break;
                default:
                    throw new Exception("Parser encountered unknown token type");
            }
        }
        private List<object> variableResolver(List<object> parameters)
        {
            for (int i = 0; i < parameters.Count(); i++)
            {
                Dictionary<string, object> component = (Dictionary<string, object>)parameters[i];
                if (!component.ContainsKey("type") || component["type"] == null)
                    throw new Exception("Parser found an operand of no type in command");

                if (!component.ContainsKey("value") || component["value"] == null)
                    throw new Exception("Parser found an operand of no value in command");

                string type = (string)component["type"];

                if (type.Equals("variable"))
                {
                    string value = (string)component["value"];
                    if (!variables.ContainsKey(value) || variables[value].Equals(null))
                        throw new Exception("Parser encountered undefined variable used as operand in function");

                    parameters[i] = new Dictionary<string, object>
                    {
                        {"type", "integer"},
                        {"value", variables[value]}
                    };
                }
            };
            return parameters;
        }

        private List<object> operatorHandler(List<object> parameters, char operatorQuery = '/', int searchIndex = -1)
        {
            int index = parameters.FindIndex(searchIndex + 1, element =>
            {
                Dictionary<string, object> component = (Dictionary<string, object>)element;
                if (!component.ContainsKey("type") || component["type"].Equals(null))
                    throw new Exception("Parser found an operand of no type in command");

                if (!component.ContainsKey("value") || component["value"].Equals(null))
                    throw new Exception("Parser found an operand of no value in command");

                string type = (string)component["type"];

                if (!type.Equals("operator")) return false;
                string value = (string)component["value"];
                char currentOperator = value[0];
                bool operatorMatch = !(currentOperator == operatorQuery);
                if (operatorMatch) return false;
                return true;
            });

            if (index == 0 || index == parameters.Count()-1)
                throw new Exception("Operation occured at beginning or end of parameter and could not be done");

            if (index != -1)
            {
                // Check that the index +/-1 is in range of length
                Dictionary<string, object> operatorComponent = (Dictionary<string, object>)parameters[index];
                Dictionary<string, object> firstComponent = (Dictionary<string, object>)parameters[index - 1];
                Dictionary<string, object> secondComponent = (Dictionary<string, object>)parameters[index + 1];

                int result = 0;
                int firstValue = (int)firstComponent["value"];
                int secondValue = (int)secondComponent["value"];

                if ("+-/*,".Contains(firstValue.ToString())|| "+-/*,".Contains(secondValue.ToString()))
                    throw new Exception("Operator was used next to another operator");

                switch (operatorComponent["value"])
                {
                    case "+":
                        result = firstValue + secondValue;
                        break;
                    case "-":
                        result = firstValue - secondValue;
                        break;
                    case "*":
                        result = firstValue * secondValue;
                        break;
                    case "/":
                        result = firstValue / secondValue;
                        break;
                }

                parameters.RemoveRange(index, 2);
                parameters[index - 1] = new Dictionary<string, object>
                            {
                                {"type", "integer"},
                                {"value", result }
                            };
                operatorHandler(parameters, operatorQuery, -1);
            }
            else
            {
                if (!operatorQuery.Equals('-'))
                {
                    string operators = "/*+-";
                    char nextOperator = operators[operators.IndexOf(operatorQuery) + 1];
                    operatorHandler(parameters, nextOperator, -1);
                }
            }
            return parameters;
        }

        private List<object> parameterHandler(List<object> parameters)
        {
            parameters = variableResolver(parameters);
            parameters = operatorHandler(parameters);
            return parameters;
        }

        private void assignmentHandler(Dictionary<string, object> token)
        {
            if (!token.ContainsKey("name") || token["name"] == null)
                throw new Exception("Parser could not find variable name in assignment token");
            if (!token.ContainsKey("parameters") || token["parameters"] == null)
                throw new Exception("Parser could not find variable parameters in assignment token");
            
            string vName = (string)token["name"];
            List<object> vParams = (List<object>)token["parameters"];
            vParams = parameterHandler(vParams);
            foreach (object item in vParams)
            {
                Dictionary<string, object> component = (Dictionary<string, object>)item;
                if (component["type"].Equals("operator") && (string)component["value"] == ",")
                    throw new Exception("Variables should only have one parameter");
            }
            int value = (int)(((Dictionary<string, object>)vParams[0])["value"]);
            if (variables.ContainsKey(vName))
                variables[vName] = value;
            else
                variables.Add(vName, value);
        }

        private void functionHandler(Dictionary<string, object> token, int operatorResolveIndex = -2, char operationSearch = '/')
        {
            if (!token.ContainsKey("name") || token["name"] == null)
                throw new Exception("Parser could not find name in assignment token");
            if (!token.ContainsKey("parameters"))
                throw new Exception("Parser could not find parameters in token");

            string fnName = (string)token["name"];
            List<object> fnParams = (List<object>)token["parameters"];
            fnParams = parameterHandler(fnParams);
            Dictionary<string, object> a = (Dictionary<string, object>)fnParams[0];
            MessageBox.Show(a["value"].ToString());
            // Check Valid Command
        }
    }
}
