using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.FormBuilderStrategies.Strategies
{
    using System.Collections;
using System.Reflection;
    using AutoForms.Helpers;
    using AutoForms.Models;
    using AutoForms.Options;

    internal class DictionaryFormGroupStrategy : BaseStrategy
    {
        private readonly StrategyResolver _strategyResolver;

        public DictionaryFormGroupStrategy(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
        {
            return PropertyFormControlTypeResolver.IsDictionary(modelType);
        }

        internal override Node Process(Type type)
        {
            if (Value == null)
            {
                return new FormGroup();
            }

            var value = (Value as IDictionary)!;

            var result = value.Keys.Cast<object>().ToDictionary(key => key.ToString(), key => BuildNode(value[key]));

            return new FormGroup
            {
                Nodes = result
            };
        }

        private Node BuildNode(object value)
        {
            return _strategyResolver.Resolve(value.GetType())
                .EnhanceWithValue(value)
                .EnhanceWithValidators(value.GetType())
                .Process(value.GetType());
        }
    }
}
