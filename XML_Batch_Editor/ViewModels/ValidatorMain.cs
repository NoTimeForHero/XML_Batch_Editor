using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;
using FluentValidation;
using FluentValidation.Validators;

namespace XML_Batch_Editor.ViewModels
{
    class ValidatorMain : AbstractValidator<VM_Main>
    {
        public ValidatorMain()
        {
            RuleFor(x => x.PathToInputDirectory).NotEmpty().WithMessage("Входная директория не может быть пустой");
            RuleFor(x => x.PathToXSD).NotEmpty().When(x => x.UseXSD).WithMessage("Путь к XSD не может быть пустым");

            RuleFor(x => x.XPath).Must(BeValidXPath).WithMessage("X-Path должен быть корректным");

            RuleFor(x => x.Search).NotEmpty().WithMessage("Строка поиска не может быть пустой");
            RuleFor(x => x.Search).Must(BeValidRegEx).When(x => x.UseRegularExpressions).WithMessage("Строка поиска должна содержать корректное регулярное выражение");
        }

        private bool BeValidRegEx(string value)
        {
            try { Regex.Match("", value); }
            catch (ArgumentException) { return false; }
            return true;
        }

        private bool BeValidXPath(string value)
        {
            try { XPathExpression.Compile(value); }
            catch (XPathException) { return false; }
            return true;
        }
    }
}
