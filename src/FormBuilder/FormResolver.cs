namespace FormBuilder
{
    using System;
    using FormBuilder.Models;
    using FormBuilder.Strategies;

    internal class FormResolver
    {
        private readonly StrategyResolver _strategyResolver;

        public FormResolver(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        public const string RootNodeName = "root";

        public Node Resolve<TModel>()
        {
            var node = Resolve(typeof(TModel));

            return node;
        }

        public Node Resolve<TModel>(TModel model)
            where TModel : class
        {
            var node = Resolve(typeof(TModel));

            return node;
        }

        public Node Resolve(Type modelType)
        {
            var node = _strategyResolver
                .Resolve(modelType)
                .Process(RootNodeName, modelType);

            return node;
        }
    }
}
