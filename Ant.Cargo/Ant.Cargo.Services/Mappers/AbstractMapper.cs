using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Services.Mappers
{
    public abstract class AbstractMapper<S, T> : Ant.Cargo.Services.Mappers.IMapper<S, T>
        where S : class
        where T : class
    {
        public virtual T MapToModel(S source, Action<S, T> extra = null)
        {
            return MapToModel<T>(source, extra);
        }

        public virtual S MapFromModel(T model, Action<T, S> extra = null, S source = null)
        {
            return MapFromModel<S>(model, extra, source);
        }

        public virtual IEnumerable<T> MapCollectionToModel(IEnumerable<S> source, Action<S, T> extra = null)
        {
            return source.Select(x => MapToModel<T>(x, extra)).ToList();
        }

        public virtual TItem MapToModel<TItem>(S source, Action<S, TItem> extra = null)
            where TItem : T
        {
            var result = Mapper.Map<TItem>(source);

            if (extra != null)
            {
                extra(source, result);
            }

            return result;
        }

        public virtual S MapFromModel<TItem>(T model, Action<T, TItem> extra = null, TItem source = null)
            where TItem : class, S
        {
            TItem result = null;

            if (source == null)
            {
                result = Mapper.Map<TItem>(model);
            }
            else
            {
                result = Mapper.Map<T, TItem>(model, source);
            }


            if (extra != null)
            {
                extra(model, result);
            }

            return result;
        }

        public virtual IEnumerable<TItem> MapCollectionToModel<TItem>(IEnumerable<S> source, Action<S, TItem> extra = null)
            where TItem : T
        {
            return source.Select(x => MapToModel<TItem>(x, extra)).ToList();
        }

        public virtual IEnumerable<S> MapCollectionFromModel<TItem>(IEnumerable<T> model, Action<T, TItem> extra = null, TItem source = null)
            where TItem : class, S
        {
            return model.Select(x => MapFromModel<TItem>(x, extra)).ToList();
        }
    }

    public static class AutomapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllUnmapped<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;

        }
    }
}
