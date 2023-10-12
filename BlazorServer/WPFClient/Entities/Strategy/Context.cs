using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClient.Entities.Strategy
{
    class Context
    {
        Logger logger = Logger.GetInstance();
        private IStrategy _strategy;

        public Context()
        {

        }

        public Context(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }
        public void DoStrategicSort(Dictionary<TypesOfShips,int> input)
        {
            var result = _strategy.StrategicSort(input);
            foreach (var keyValuePair in result as Dictionary<TypesOfShips, int>)
            {          
                logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}, typeof(parameter): {_strategy.GetType()} response: Type: {keyValuePair.Key} Amount: {keyValuePair.Value}");
            }
        }
    }
}
