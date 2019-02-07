using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AspNetCoreWithMongoDb.Attribute;
using AspNetCoreWithMongoDb.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver;

namespace AspNetCoreWithMongoDb.Extentions
{
    public static class MonogCollectionExtentions
    {
        public static List<T> Include<T,E>(this List<T> collection,IMongoContext context)
        {
            
            //IMongoCollection<> secondCollection = context.GetType().GetProperties()
            //    .FirstOrDefault(i => i.Name == "Categories").GetValue(context);

            //var secondCollectionData = (IMongoCollection<T> secondCollection)

            foreach (var book in collection)
            {
                var navigationProp = book.GetType().GetProperties().FirstOrDefault(i=> typeof(E).Name.Equals(i.Name));
                var navigationPropAttr = (MongoRefAttribute) navigationProp.GetCustomAttributes(typeof(MongoRefAttribute), false).FirstOrDefault();
                var value = default(E);
                var secondCollection = (IMongoCollection<E>) context.GetType().GetProperty(navigationPropAttr.Table).GetValue(context);
                var secondCollectionValue = secondCollection.AsQueryable().ToList()
                    .FirstOrDefault(i => i.GetType().GetProperty(navigationPropAttr.Id).GetValue(i)
                        .Equals(book.GetType().GetProperty(navigationPropAttr.RefId).GetValue(book)));
                value = secondCollectionValue;
                typeof(T).GetProperty(navigationProp.Name).SetValue(book, value);
            }
            return collection;
        }

        //public static List<T> IncludeAll<T>(this List<T> collection, IMongoContext context)
        //{

        //    //IMongoCollection<> secondCollection = context.GetType().GetProperties()
        //    //    .FirstOrDefault(i => i.Name == "Categories").GetValue(context);

        //    //var secondCollectionData = (IMongoCollection<T> secondCollection)
        //    var navigationProperties = typeof(T).GetProperties()
        //        .Where(i => i.GetCustomAttributes(typeof(MongoRefAttribute), false).Any());
        //    foreach (var navigationProperty in navigationProperties)
        //    {
        //        var navigationPropAttr = (MongoRefAttribute)navigationProperty.GetCustomAttributes(typeof(MongoRefAttribute), false).FirstOrDefault();
        //        foreach (var book in collection)
        //        {
        //            var value = navigationProperty.GetValue(book);
        //            var secondCollection = context.GetType().GetProperty(navigationPropAttr.Table).GetValue(context);
        //        }

        //    }


        //    foreach (var book in collection)
        //    {
        //        var navigationProp = book.GetType().GetProperties().FirstOrDefault(i => typeof(E).Name.Equals(i.Name));
        //        var navigationPropAttr = (MongoRefAttribute)navigationProp.GetCustomAttributes(typeof(MongoRefAttribute), false).FirstOrDefault();
        //        var value = default(E);
        //        var secondCollection = (IMongoCollection<E>)context.GetType().GetProperty(navigationPropAttr.Table).GetValue(context);
        //        foreach (var second in secondCollection.Find(i => true).ToList())
        //        {
        //            if (second.GetType().GetProperty(navigationPropAttr.Id).GetValue(second).Equals(book.GetType().GetProperty(navigationPropAttr.RefId).GetValue(book)))
        //            {
        //                value = second;
        //            }
        //        }
        //        typeof(T).GetProperty(navigationProp.Name).SetValue(book, value);
        //    }
        //    return collection;
        //}
    }
}