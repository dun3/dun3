using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.IO;

namespace Com.Hertkorn.Tools.Generator.FlagHelper
{
    public class Generator
    {
        public string NamespaceName { get; set; }
        public string ClassName { get; set; }

        public Generator()
            : this("Default", "FlagHelper")
        {
        }
        public Generator(string namespaceName, string className)
        {
            NamespaceName = namespaceName;
            ClassName = className;
        }

        public void Render(TextWriter writer)
        {
            CodeGeneratorOptions codeOpts = new CodeGeneratorOptions();
            codeOpts.BlankLinesBetweenMembers = true;
            codeOpts.BracingStyle = "C";
            //write code
            CodeCompileUnit unit = GenerateCodeCompileUnit();
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeGenerator generator = codeProvider.CreateGenerator(writer);
            generator.GenerateCodeFromCompileUnit(unit, writer, codeOpts);
        }
        public CodeCompileUnit GenerateCodeCompileUnit()
        {
            //Create a compile unit for the namespace and imports for your new code
            CodeCompileUnit unit = new CodeCompileUnit();

            CodeNamespace codeNamespace = BuildCodeNamespace();
            unit.Namespaces.Add(codeNamespace);

            CodeTypeDeclaration codeTypeDeclaration = BuildCodeTypeDeclaration();
            codeNamespace.Types.Add(codeTypeDeclaration);
            
            if (!m_generateEmptyPartialClass)
            {
                ctd.BaseTypes.AddRange(generateBaseTypes(classMapping));

                ctd.Members.AddRange(doFields(classMapping, class2classmap));
                //default constructor
                ctd.Members.Add(DefaultConstructor());
                if (classMapping.needsMinimalConstructor())
                {
                    ctd.Members.Add(MinimalConstructor(classMapping));
                }
                //field populating constructor
                ctd.Members.Add(AssignmentConstructor(classMapping));

                ctd.Members.AddRange(doProperties(classMapping, class2classmap));
            }
            else
            {
                // TODO: ist .Fields wirklich richtig?
                if (NetTool.hasGenericEntityList(classMapping.Fields))
                {
                    FieldProperty[] EntityListFields = NetTool.findAllGenericEntityLists(classMapping.Fields);

                    CodeMemberMethod method1 = new CodeMemberMethod();
                    method1.Attributes = MemberAttributes.Private;
                    method1.Name = "WireUpEntities";
                    foreach (FieldProperty prop in EntityListFields)
                    {
                        //TODO: Typen einbauen
                        method1.Statements.Add(
                            new CodeCommentStatement(
                                " Sampleimplement parent/child relationship add/remove scaffolding between One and Many in One2Many"));

                        method1.Statements.Add(
                            new CodeCommentStatement(
                                String.Format(" {0} = new {1}<{2}>(",
                                              ToInternalName(prop.fieldcase),
                                              shortenType("NHibernate.Generics.EntityList", classMapping.Imports, classMapping),
                                              shortenType(prop.ForeignClass.FullyQualifiedName, classMapping.Imports, classMapping))));
                        method1.Statements.Add(
                            new CodeCommentStatement(
                                " delegate(" + shortenType(prop.ForeignClass.FullyQualifiedName, classMapping.Imports, classMapping) +
                                " entityList) { entityList.[EntityRefProperty] = this; },"));
                        method1.Statements.Add(
                            new CodeCommentStatement(
                                " delegate(" + shortenType(prop.ForeignClass.FullyQualifiedName, classMapping.Imports, classMapping) +
                                " entityList) { entityList.[EntityRefProperty] = null; });"));


                        //method1.Statements.Add(
                        //    new CodeCommentStatement(
                        //        "// Sampleimplement parent/child relationship add/remove scaffolding between One and Many in One2Many"));
                        //method1.Statements.Add(new CodeCommentStatement("_mappers = new EntityList<IMapWebContent>("));
                        //method1.Statements.Add(
                        //    new CodeCommentStatement("delegate(IMapWebContent mapWebContent) { mapWebContent.Content = this; },"));
                        //method1.Statements.Add(
                        //    new CodeCommentStatement("delegate(IMapWebContent mapWebContent) { mapWebContent.Content = null; }"));


                        ctd.Members.Add(method1);

                    }
                }
                if (NetTool.hasGenericEntityRef(classMapping.Fields))
                {
                    FieldProperty[] EntityRefFields = NetTool.findAllGenericEntityRefs(classMapping.Fields);

                    CodeMemberMethod method = new CodeMemberMethod();
                    method.Attributes = MemberAttributes.Private;
                    method.Name = "WireUpEntities";
                    foreach (FieldProperty prop in EntityRefFields)
                    {
                        //TODO: Typen einbauen
                        method.Statements.Add(
                            new CodeCommentStatement(
                                " Sampleimplement parent/child relationship add/remove scaffolding between One and Many in One2Many"));

                        method.Statements.Add(
                            new CodeCommentStatement(
                                String.Format(" {0} = new {1}<{2}>(",
                                              ToInternalName(prop.fieldcase),
                                              shortenType("NHibernate.Generics.EntityRef", classMapping.Imports, classMapping),
                                              shortenType(prop.FullyQualifiedTypeName, classMapping.Imports, classMapping))));
                        method.Statements.Add(
                            new CodeCommentStatement(
                                " delegate(" + shortenType(prop.FullyQualifiedTypeName, classMapping.Imports, classMapping) +
                                " entityRef) { entityRef.[EntityListProperty].Add(this); },"));
                        method.Statements.Add(
                            new CodeCommentStatement(
                                " delegate(" + shortenType(prop.FullyQualifiedTypeName, classMapping.Imports, classMapping) +
                                " entityRef) { entityRef.[EntityListProperty].Remove(this); });"));


                        //method.Statements.Add(new CodeCommentStatement("_content = new EntityRef<IWebContent>("));
                        //method.Statements.Add(
                        //    new CodeCommentStatement("delegate(IWebContent webContent) { webContent.Mappers.Add(this); },"));
                        //method.Statements.Add(
                        //    new CodeCommentStatement("delegate(IWebContent webContent) { webContent.Mappers.Remove(this); }"));

                        ctd.Members.Add(method);

                    }
                }
            }

            



        }

        private CodeTypeDeclaration BuildCodeTypeDeclaration()
        {
            //Build the class declaration and member variables			
            CodeTypeDeclaration ctd = new CodeTypeDeclaration();
            ctd.IsClass = true;
            ctd.Name = ClassName;
            ctd.IsPartial = true;
            ctd.Attributes = MemberAttributes.Public;
            return ctd;
        }

        private CodeNamespace BuildCodeNamespace()
        {
            CodeNamespace myNamespace = new CodeNamespace(NamespaceName);

            //TODO: prüfen ob nötig, wird ab 2.0 evtl. automatisch referenziert??
            myNamespace.Imports.Add(new CodeNamespaceImport("System"));

            myNamespace.Comments.Add(new CodeCommentStatement(String.Format("autogenerated: {0}", DateTime.Now)));
            return myNamespace;
        }
    }
}
