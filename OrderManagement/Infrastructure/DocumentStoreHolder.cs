using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;

namespace OrderManagement.Web.Infrastructure
{
    public class DocumentStoreHolder
    {
        private static IDocumentStore documentStore;

        public static IDocumentStore DocumentStore
        {
            get { return (documentStore ?? (documentStore = CreateDocumentStore())); }
        }

        private static IDocumentStore CreateDocumentStore()
        {
            var store = new DocumentStore
            {
                ConnectionStringName = "OrderManagment"
            }.Initialize();

            return store;
        }

        private static readonly ConcurrentDictionary<Type, Accessors> AccessorsCache = new ConcurrentDictionary<Type, Accessors>();
        public static IDocumentSession TryAddSession(object instance)
        {
            var accessors = AccessorsCache.GetOrAdd(instance.GetType(), CreateAccessorsForType);

            if (accessors == null)
                return null;

            var documentSession = DocumentStore.OpenSession();
            accessors.Set(instance, documentSession);

            return documentSession;
        }

        public static void TryComplete(object instance, bool succcessfully)
        {
            Accessors accesors;
            if (AccessorsCache.TryGetValue(instance.GetType(), out accesors) == false || accesors == null)
                return;

            using (var documentSession = accesors.Get(instance))
            {
                if (documentSession == null)
                    return;

                if (succcessfully)
                    documentSession.SaveChanges();
            }
        }
        private static Accessors CreateAccessorsForType(Type type)
        {
            var sessionProp =
                type.GetProperties().FirstOrDefault(
                    x => x.PropertyType == typeof(IDocumentSession) && x.CanRead && x.CanWrite);
            if (sessionProp == null)
                return null;

            return new Accessors
            {
                Set = (instance, session) => sessionProp.SetValue(instance, session, null),
                Get = instance => (IDocumentSession)sessionProp.GetValue(instance, null)
            };
        }

        private class Accessors
        {
            public Action<object, IDocumentSession> Set;
            public Func<object, IDocumentSession> Get;
        }

     
    }
}