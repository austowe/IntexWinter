using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IntexWinter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        // Dynamically create page links
        private IUrlHelperFactory uhf;

        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        // Converts tag into csharp
        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassEnabled { get; set; }
        public string PageClassDisabled { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            TagBuilder first = new TagBuilder("a");
            TagBuilder prev = new TagBuilder("a");
            TagBuilder currentLess2 = new TagBuilder("a");
            TagBuilder currentLess1 = new TagBuilder("a");
            TagBuilder current = new TagBuilder("a");
            TagBuilder currentPlus1 = new TagBuilder("a");
            TagBuilder currentPlus2 = new TagBuilder("a");
            TagBuilder next = new TagBuilder("a");
            TagBuilder last = new TagBuilder("a");



            // Enabled buttons if the current page is more than 1
            if (PageModel.CurrentPage > 1)
            {
                first.Attributes["href"] = uh.Action(PageAction, new { pageNum = 1 });
                first.AddCssClass(PageClass); first.AddCssClass(PageClassEnabled);
                first.InnerHtml.Append("First");

                prev.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 1 });
                prev.AddCssClass(PageClass); prev.AddCssClass(PageClassEnabled);
                prev.InnerHtml.Append("Prev");

                //Enabled button if the current page is more than 2
                if (PageModel.CurrentPage > 2)
                {
                    currentLess2.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 2 });
                    currentLess2.AddCssClass(PageClass); currentLess2.AddCssClass(PageClassEnabled);
                    currentLess2.InnerHtml.Append((PageModel.CurrentPage - 2).ToString());
                }
                else // Disabled buttons if current page is 2
                {
                    currentLess2.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 2 });
                    currentLess2.AddCssClass(PageClass); currentLess2.AddCssClass(PageClassDisabled);
                    currentLess2.InnerHtml.Append("--");
                }

                currentLess1.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 1 });
                currentLess1.AddCssClass(PageClass); currentLess1.AddCssClass(PageClassEnabled);
                currentLess1.InnerHtml.Append((PageModel.CurrentPage - 1).ToString());
            } 
            else // Disabled buttons if current page is 1
            {
                first.Attributes["href"] = uh.Action(PageAction, new { pageNum = 1 });
                first.AddCssClass(PageClass); first.AddCssClass(PageClassDisabled);
                first.InnerHtml.Append("-----");

                prev.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 1 });
                prev.AddCssClass(PageClass); prev.AddCssClass(PageClassDisabled);
                prev.InnerHtml.Append("-----");

                currentLess2.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 2 });
                currentLess2.AddCssClass(PageClass); currentLess2.AddCssClass(PageClassDisabled);
                currentLess2.InnerHtml.Append("--");

                currentLess1.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 1 });
                currentLess1.AddCssClass(PageClass); currentLess1.AddCssClass(PageClassDisabled);
                currentLess1.InnerHtml.Append("--");
            }
            
            current.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage });
            current.AddCssClass(PageClass); current.AddCssClass(PageClassSelected);
            current.InnerHtml.Append(PageModel.CurrentPage.ToString());
            
            if (PageModel.CurrentPage < PageModel.TotalPages)
            {
                currentPlus1.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage + 1 });
                currentPlus1.AddCssClass(PageClass); currentPlus1.AddCssClass(PageClassEnabled);
                currentPlus1.InnerHtml.Append((PageModel.CurrentPage + 1).ToString());

                if (PageModel.CurrentPage < PageModel.TotalPages - 1)
                {
                    currentPlus2.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage + 2 });
                    currentPlus2.AddCssClass(PageClass); currentPlus2.AddCssClass(PageClassEnabled);
                    currentPlus2.InnerHtml.Append((PageModel.CurrentPage + 2).ToString());
                }
                else
                {
                    currentPlus2.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage + 2 });
                    currentPlus2.AddCssClass(PageClass); currentPlus2.AddCssClass(PageClassDisabled);
                    currentPlus2.InnerHtml.Append("---");
                }

                next.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage + 1 });
                next.AddCssClass(PageClass); next.AddCssClass(PageClassEnabled);
                next.InnerHtml.Append("Next");

                last.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.TotalPages });
                last.AddCssClass(PageClass); last.AddCssClass(PageClassEnabled);
                last.InnerHtml.Append("Last");
            }
            else
            {
                currentPlus1.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage + 1 });
                currentPlus1.AddCssClass(PageClass); currentPlus1.AddCssClass(PageClassDisabled);
                currentPlus1.InnerHtml.Append("---");

                currentPlus2.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage + 2 });
                currentPlus2.AddCssClass(PageClass); currentPlus2.AddCssClass(PageClassDisabled);
                currentPlus2.InnerHtml.Append("---");

                next.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage + 1 });
                next.AddCssClass(PageClass); next.AddCssClass(PageClassDisabled);
                next.InnerHtml.Append("-----");

                last.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.TotalPages });
                last.AddCssClass(PageClass); last.AddCssClass(PageClassDisabled);
                last.InnerHtml.Append("----");
            }

            

            final.InnerHtml.AppendHtml(first);
            final.InnerHtml.AppendHtml(prev);
            final.InnerHtml.AppendHtml(currentLess2);
            final.InnerHtml.AppendHtml(currentLess1);
            final.InnerHtml.AppendHtml(current);
            final.InnerHtml.AppendHtml(currentPlus1);
            final.InnerHtml.AppendHtml(currentPlus2);
            final.InnerHtml.AppendHtml(next);
            final.InnerHtml.AppendHtml(last);
            
            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
