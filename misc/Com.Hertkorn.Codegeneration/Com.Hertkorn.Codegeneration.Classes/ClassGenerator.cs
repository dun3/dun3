using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.CodeDom;
using System.Reflection;
using System.CodeDom.Compiler;

namespace Com.Hertkorn.Codegeneration.Classes
{
    public class ClassGenerator
    {
        public ClassGenerator(string namespaceName)
        {
            Namespace = namespaceName;
        }

        public string Namespace { get; set; }

        private GeneratedPart GenerateSimple(ClassDefinition classDefinition)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace(Namespace);
            AddDefaultImports(samples.Imports);

            CodeTypeDeclaration targetClass = new CodeTypeDeclaration(classDefinition.Name);

            targetClass.IsClass = classDefinition.IsClass;

            targetClass.TypeAttributes = TypeAttributes.Public;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);


            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;

            //// Add parameters.
            //constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Double), "width"));
            //constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Double), "height"));

            //// Add field initialization logic
            //CodeFieldReferenceExpression widthReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "widthValue");
            //constructor.Statements.Add(new CodeAssignStatement(widthReference, new CodeArgumentReferenceExpression("width")));
            //CodeFieldReferenceExpression heightReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "heightValue");
            //constructor.Statements.Add(new CodeAssignStatement(heightReference, new CodeArgumentReferenceExpression("height")));

            targetClass.Members.Add(constructor);

            StringWriter writer = new StringWriter();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            provider.GenerateCodeFromCompileUnit(targetUnit, writer, options);

            return new GeneratedPart() { GeneratedCode = writer.ToString(), Name = classDefinition.Name + ".cs" };
        }

        public List<GeneratedPart> Generate(ClassDefinition classDefinition)
        {
            List<GeneratedPart> generatedPartz = new List<GeneratedPart>();

            if (!classDefinition.IsPartial)
            {
                generatedPartz.Add(GenerateSimple(classDefinition));
            }
            else
            {
                generatedPartz.Add(GenerateHumanPartial(classDefinition));
                generatedPartz.Add(GenerateBehindPartial(classDefinition));
            }
            return generatedPartz;
        }

        private GeneratedPart GenerateBehindPartial(ClassDefinition classDefinition)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace(Namespace);
            AddDefaultImports(samples.Imports);

            CodeTypeDeclaration targetClass = new CodeTypeDeclaration(classDefinition.Name);

            targetClass.IsPartial = true;
            targetClass.IsClass = classDefinition.IsClass;

            targetClass.TypeAttributes = TypeAttributes.Public;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);


            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;

            //// Add parameters.
            //constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Double), "width"));
            //constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Double), "height"));

            //// Add field initialization logic
            //CodeFieldReferenceExpression widthReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "widthValue");
            //constructor.Statements.Add(new CodeAssignStatement(widthReference, new CodeArgumentReferenceExpression("width")));
            //CodeFieldReferenceExpression heightReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "heightValue");
            //constructor.Statements.Add(new CodeAssignStatement(heightReference, new CodeArgumentReferenceExpression("height")));

            targetClass.Members.Add(constructor);

            StringWriter writer = new StringWriter();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            provider.GenerateCodeFromCompileUnit(targetUnit, writer, options);

            return new GeneratedPart() { Name = classDefinition.Name + ".generated.cs", GeneratedCode = writer.ToString() };
        }

        private GeneratedPart GenerateHumanPartial(ClassDefinition classDefinition)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace(Namespace);
            AddDefaultImports(samples.Imports);

            CodeTypeDeclaration targetClass = new CodeTypeDeclaration(classDefinition.Name);

            targetClass.IsClass = classDefinition.IsClass;
            targetClass.IsPartial = true;
            targetClass.TypeAttributes = TypeAttributes.Public;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);

            StringWriter writer = new StringWriter();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            provider.GenerateCodeFromCompileUnit(targetUnit, writer, options);

            return new GeneratedPart() { Name = classDefinition.Name + ".cs", GeneratedCode = writer.ToString() };
        }

        protected virtual void AddDefaultImports(CodeNamespaceImportCollection imports)
        {
            imports.Add(new CodeNamespaceImport("System"));
        }
    }
}
