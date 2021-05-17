using System;
using System.Collections.Generic;

namespace web.api.demarcacao.terreno.CrossCutting
{
    public static class UtilExtensions
    {
        public static void ForEach<TEntity>(this IEnumerable<TEntity> entities, Action<TEntity> action)
        {
            foreach (var entity in entities)
            {
                action(entity);
            }
        }
    }
}
