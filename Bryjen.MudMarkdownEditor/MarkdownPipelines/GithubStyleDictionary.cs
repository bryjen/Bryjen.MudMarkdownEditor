using ColorCode.Common;
using ColorCode.Styling;

namespace Bryjen.MudMarkdownEditor.MarkdownPipelines;

internal static  class GithubStyleDictionary
{
    private const string GithubBackground = "#262c36";
    private const string GithubPlainText = "#d1d7e0";

    private const string GithubComment = "#9198a1";
    private const string GithubKeyword = "#f47061";
    private const string VsDarkGray = "#FF9B9B9B";
    private const string GithubNumber = "#6cb6ff";
    private const string VsDarkClass = "#FF4EC9B0";
    private const string GithubString = "#96d0ff";
    
    public static StyleDictionary GithubStyle =>
        new StyleDictionary
        {
            new Style(ScopeName.PlainText)
            {
                Foreground = GithubPlainText,
                Background = GithubBackground,
                ReferenceName = "plainText"
            },
            new Style(ScopeName.HtmlServerSideScript)
            {
                Background = StyleDictionary.Yellow,
                ReferenceName = "htmlServerSideScript"
            },
            new Style(ScopeName.HtmlComment)
            {
                Foreground = GithubComment,
                ReferenceName = "htmlComment"
            },
            new Style(ScopeName.HtmlTagDelimiter)
            {
                Foreground = GithubKeyword,
                ReferenceName = "htmlTagDelimiter"
            },
            new Style(ScopeName.HtmlElementName)
            {
                Foreground = StyleDictionary.DullRed,
                ReferenceName = "htmlElementName"
            },
            new Style(ScopeName.HtmlAttributeName)
            {
                Foreground = StyleDictionary.Red,
                ReferenceName = "htmlAttributeName"
            },
            new Style(ScopeName.HtmlAttributeValue)
            {
                Foreground = GithubKeyword,
                ReferenceName = "htmlAttributeValue"
            },
            new Style(ScopeName.HtmlOperator)
            {
                Foreground = GithubKeyword,
                ReferenceName = "htmlOperator"
            },
            new Style(ScopeName.Comment)
            {
                Foreground = GithubComment,
                ReferenceName = "comment"
            },
            new Style(ScopeName.XmlDocTag)
            {
                Foreground = GithubComment,
                ReferenceName = "xmlDocTag"
            },
            new Style(ScopeName.XmlDocComment)
            {
                Foreground = GithubComment,
                ReferenceName = "xmlDocComment"
            },
            new Style(ScopeName.String)
            {
                Foreground = GithubString,
                ReferenceName = "string"
            },
            new Style(ScopeName.StringCSharpVerbatim)
            {
                Foreground = GithubString,
                ReferenceName = "stringCSharpVerbatim"
            },
            new Style(ScopeName.Keyword)
            {
                Foreground = GithubKeyword,
                ReferenceName = "keyword"
            },
            new Style(ScopeName.PreprocessorKeyword)
            {
                Foreground = GithubKeyword,
                ReferenceName = "preprocessorKeyword"
            },
            new Style(ScopeName.HtmlEntity)
            {
                Foreground = StyleDictionary.Red,
                ReferenceName = "htmlEntity"
            },
            new Style(ScopeName.JsonKey)
            {
                Foreground = StyleDictionary.DarkOrange,
                ReferenceName = "jsonKey"
            },
            new Style(ScopeName.JsonString)
            {
                Foreground = StyleDictionary.DarkCyan,
                ReferenceName = "jsonString"
            },
            new Style(ScopeName.JsonNumber)
            {
                Foreground = StyleDictionary.BrightGreen,
                ReferenceName = "jsonNumber"
            },
            new Style(ScopeName.JsonConst)
            {
                Foreground = StyleDictionary.BrightPurple,
                ReferenceName = "jsonConst"
            },
            new Style(ScopeName.XmlAttribute)
            {
                Foreground = GithubComment,
                ReferenceName = "xmlAttribute"
            },
            new Style(ScopeName.XmlAttributeQuotes)
            {
                Foreground = GithubKeyword,
                ReferenceName = "xmlAttributeQuotes"
            },
            new Style(ScopeName.XmlAttributeValue)
            {
                Foreground = GithubKeyword,
                ReferenceName = "xmlAttributeValue"
            },
            new Style(ScopeName.XmlCDataSection)
            {
                Foreground = GithubComment,
                ReferenceName = "xmlCDataSection"
            },
            new Style(ScopeName.XmlComment)
            {
                Foreground = GithubComment,
                ReferenceName = "xmlComment"
            },
            new Style(ScopeName.XmlDelimiter)
            {
                Foreground = GithubComment,
                ReferenceName = "xmlDelimiter"
            },
            new Style(ScopeName.XmlName)
            {
                Foreground = GithubComment,
                ReferenceName = "xmlName"
            },
            new Style(ScopeName.ClassName)
            {
                Foreground = VsDarkClass,
                ReferenceName = "className"
            },
            new Style(ScopeName.CssSelector)
            {
                Foreground = StyleDictionary.DullRed,
                ReferenceName = "cssSelector"
            },
            new Style(ScopeName.CssPropertyName)
            {
                Foreground = StyleDictionary.Red,
                ReferenceName = "cssPropertyName"
            },
            new Style(ScopeName.CssPropertyValue)
            {
                Foreground = GithubKeyword,
                ReferenceName = "cssPropertyValue"
            },
            new Style(ScopeName.SqlSystemFunction)
            {
                Foreground = StyleDictionary.Magenta,
                ReferenceName = "sqlSystemFunction"
            },
            new Style(ScopeName.PowerShellAttribute)
            {
                Foreground = StyleDictionary.PowderBlue,
                ReferenceName = "powershellAttribute"
            },
            new Style(ScopeName.PowerShellOperator)
            {
                Foreground = VsDarkGray,
                ReferenceName = "powershellOperator"
            },
            new Style(ScopeName.PowerShellType)
            {
                Foreground = StyleDictionary.Teal,
                ReferenceName = "powershellType"
            },
            new Style(ScopeName.PowerShellVariable)
            {
                Foreground = StyleDictionary.OrangeRed,
                ReferenceName = "powershellVariable"
            },
            new Style(ScopeName.PowerShellCommand)
            {
                Foreground = StyleDictionary.Yellow,
                ReferenceName = "powershellCommand"
            },
            new Style(ScopeName.PowerShellParameter)
            {
                Foreground = VsDarkGray,
                ReferenceName = "powershellParameter"
            },
            new Style(ScopeName.Type)
            {
                Foreground = StyleDictionary.Teal,
                ReferenceName = "type"
            },
            new Style(ScopeName.TypeVariable)
            {
                Foreground = StyleDictionary.Teal,
                Italic = true,
                ReferenceName = "typeVariable"
            },
            new Style(ScopeName.NameSpace)
            {
                Foreground = StyleDictionary.Navy,
                ReferenceName = "namespace"
            },
            new Style(ScopeName.Constructor)
            {
                Foreground = StyleDictionary.Purple,
                ReferenceName = "constructor"
            },
            new Style(ScopeName.Predefined)
            {
                Foreground = StyleDictionary.Navy,
                ReferenceName = "predefined"
            },
            new Style(ScopeName.PseudoKeyword)
            {
                Foreground = StyleDictionary.Navy,
                ReferenceName = "pseudoKeyword"
            },
            new Style(ScopeName.StringEscape)
            {
                Foreground = VsDarkGray,
                ReferenceName = "stringEscape"
            },
            new Style(ScopeName.ControlKeyword)
            {
                Foreground = GithubKeyword,
                ReferenceName = "controlKeyword"
            },
            new Style(ScopeName.Number)
            {
                ReferenceName = "number",
                Foreground = GithubNumber
            },
            new Style(ScopeName.Operator)
            {
                ReferenceName = "operator"
            },
            new Style(ScopeName.Delimiter)
            {
                ReferenceName = "delimiter"
            },
            new Style(ScopeName.MarkdownHeader)
            {
                Foreground = GithubKeyword,
                Bold = true,
                ReferenceName = "markdownHeader"
            },
            new Style(ScopeName.MarkdownCode)
            {
                Foreground = GithubString,
                ReferenceName = "markdownCode"
            },
            new Style(ScopeName.MarkdownListItem)
            {
                Bold = true,
                ReferenceName = "markdownListItem"
            },
            new Style(ScopeName.MarkdownEmph)
            {
                Italic = true,
                ReferenceName = "italic"
            },
            new Style(ScopeName.MarkdownBold)
            {
                Bold = true,
                ReferenceName = "bold"
            },
            new Style(ScopeName.BuiltinFunction)
            {
                Foreground = StyleDictionary.OliveDrab,
                Bold = true,
                ReferenceName = "builtinFunction"
            },
            new Style(ScopeName.BuiltinValue)
            {
                Foreground = StyleDictionary.DarkOliveGreen,
                Bold = true,
                ReferenceName = "builtinValue"
            },
            new Style(ScopeName.Attribute)
            {
                Foreground = StyleDictionary.DarkCyan,
                Italic = true,
                ReferenceName = "attribute"
            },
            new Style(ScopeName.SpecialCharacter)
            {
                ReferenceName = "specialChar"
            },
        };
}