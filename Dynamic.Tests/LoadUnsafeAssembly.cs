using System;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Dynamic.Tests {
    public class LoadUnsafeAssembly {
        private ITestOutputHelper _output;
        public LoadUnsafeAssembly(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void InvokeAssemblyMethod() {
            string filePath = @"C:\Users\nickc\Dropbox\dynamic\dynamic\bin\Release\Dynamic.dll";
            Assembly unsafeAssembly = Assembly.LoadFile(filePath);
            Type unsafeType = unsafeAssembly.GetType("Dynamic.LoadMeLater");
            object unsafeTypeInstance = Activator.CreateInstance(unsafeType);

            var result = unsafeType.InvokeMember("InvokeMeLater", 
                BindingFlags.InvokeMethod |
                BindingFlags.Instance |
                BindingFlags.Public, 
                null, 
                unsafeTypeInstance, 
                null).ToString();

            _output.WriteLine(result);
        }

        [Fact]
        public void InvokeAssemblyMethodDynamic() {
            string filePath = @"C:\Users\nickc\Dropbox\dynamic\dynamic\bin\Release\Dynamic.dll";
            Assembly unsafeAssembly = Assembly.LoadFile(filePath);
            Type unsafeType = unsafeAssembly.GetType("Dynamic.LoadMeLater");
            dynamic unsafeTypeInstance = Activator.CreateInstance(unsafeType);

            var result = unsafeTypeInstance.InvokeMeLater();

            _output.WriteLine(result);
        }
    }
}
