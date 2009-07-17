<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="miTab.aspx.cs" Inherits="Tabs_miTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">


        <script type="text/javascript">
            $(function() {
 
                $('#container-4').tabs({ fxFade: true, fxSpeed: 'fast' });
                
            });
        </script>

 
    
        <div id="container-4">
            <ul>
                <li><a href="#fragment-10"><span>One</span></a></li>
                <li><a href="#fragment-11"><span>Two</span></a></li>
                <li><a href="#fragment-12"><span>Three</span></a></li>
            </ul>
            <div id="fragment-10">
                <p>
                    Use a fade effect to switch tabs.
                    You can optionally specify the speed for the animation with the option <code>fxSpeed: value</code>.
                    The value is either a string of one of the predefined speeds in jQuery (<code>slow</code>,
                    <code>normal</code>, <code>fast</code>) or an integer value specifying the duration for the animation
                    in milliseconds. If omitted it defaults to <code>normal</code>.
                </p>
                <pre><code>$(&#039;#container&#039;).tabs({ fxFade: true, fxSpeed: 'fast' });</code></pre>
            </div>
            <div id="fragment-11">
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
            </div>
            <div id="fragment-12">
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
            </div>
        </div>

       

</asp:Content>

