﻿using System;
using System.Collections.Generic;

namespace MyLibrary
{
    record Foo
    {
        public void SomePublicMethod(string s1, string s2) { }

        protected void SomeProtectedMethod(string s1, string s2) { }

        private void SomePrivateMethod(string s1, string s2) { }

        public void GenericMethod<T>(T s1, string s2) { }

        public void GenericMethod2<T>(IEnumerable<T> s1, string s2) { }
    }

    record Bar : Foo
    {
        public void SomePublicMethod(string s1, object s2) { } // FN {{Remove or rename that method because it hides 'MyLibrary.Foo.SomePublicMethod(string, string)'.}}
        protected void SomeProtectedMethod(string s1, object o2) { } // FN

        private void SomePrivateMethod(string s1, object o2) { }

        public void SomePublicMethod(string s1, string s2) { }

        public void GenericMethod<TType>(TType s1, object s2) { } // FN

        public void GenericMethod2<T>(IEnumerable<T> s1, object s2) { } // FN
    }

    record Bar2 : Foo
    {
    }

    record Bar3 : Bar2
    {
        public void SomePublicMethod(string s1, object o2) { } // FN
    }


    record MultipleOverloadsBase
    {
        public bool Method1(object obj) => true;

        public bool Method1(string obj) => true;
    }

    record MultipleOverloadsDerived : MultipleOverloadsBase
    {
        public bool Method1(object obj) => true;
    }
}

namespace OtherNamespace
{
    record Class1
    {
        internal void SomeMethod(string s) { }
    }

    record Class2 : Class1
    {
        void SomeMethod(object s) { } // FN
    }
}

namespace MyNamespace
{
    record Class3 : OtherNamespace.Class1
    {
        void SomeMethod(object s) { }
    }
}
