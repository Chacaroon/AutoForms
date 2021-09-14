using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FormBuilder.UnitTests")]
namespace FormBuilder.Strategies
{
    using System;
    using System.Linq;
    using System.Reflection;
    using FormBuilder.Helpers;
    using FormBuilder.Models;

    internal class FormGroupStrategy : BaseStrategy
    {
        private readonly StrategyResolver _strategyResolver;

        public FormGroupStrategy(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }
        
        internal override bool IsStrategyApplicable(Type modelType) => PropertyFormControlTypeResolver.IsFormGroup(modelType);

        internal override Node Process(string name, Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var nodes = properties.Select(x => _strategyResolver.Resolve(x.PropertyType).Process(x.Name, x.PropertyType));

            return new FormGroup
            {
                Name = name,
                Nodes = nodes
            };
        }
    }
}
