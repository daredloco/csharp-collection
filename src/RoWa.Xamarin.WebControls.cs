using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;

namespace RoWa.Xamarin
{
	namespace WebControls
	{
		namespace WebViews
		{
			public class BootstrapJSView : WebView
			{
				public EventHandler OnLoadingDone => client.OnLoadingDone;
				public EventHandler<WebViewClients.ActionFiredArgs> OnActionFired { get { return client.OnActionFired; } set { client.OnActionFired = value; } }

				public Dictionary<string, Action<string>> JSActions = new Dictionary<string, Action<string>>();

				private WebViewClients.WebClientPlus client;

				public BootstrapJSView(Context context) : base(context)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapJSView(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapJSView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapJSView(Context context, Android.Util.IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapJSView(Activity activity) : base(activity.ApplicationContext)
				{
					CreateClient(activity);
					StartUp();
				}

				private void StartUp()
				{
					Settings.JavaScriptEnabled = true;
					Settings.DomStorageEnabled = true;
					Settings.AllowFileAccess = true;
					Settings.AllowFileAccessFromFileURLs = true;
					Settings.BuiltInZoomControls = true;
				}

				public void CreateClient(Activity activity)
				{
					client = new WebViewClients.WebClientPlus(activity);
					SetWebViewClient(client);
				}

				public void AddJSAction(string key, Action<string> action)
				{
					JSActions.Add(key, action);
				}

				public void RemoveJSAction(string key)
				{
					JSActions.Remove(key);
				}

				public void InvokeJSAction(string key, string data)
				{
					if(!JSActions.ContainsKey(key) || data == null)
					{
						return;
					}
					JSActions[key].Invoke(data);
				}

				public void AddAction(string key, Intent intent = null)
				{
					client.AddAction(key, intent);
				}

				public void RemoveAction(string key)
				{
					client.RemoveAction(key);
				}

				public void Show(string html)
				{
					string fullhtml = "<!DOCTYPE html>\n";
					fullhtml += "<html>\n";
					fullhtml += "<head>\n";
					fullhtml += "<meta charset=\"utf-8\">\n";
					fullhtml += "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\n";
					fullhtml += "<link href=\"file:///android_asset/bootstrap/bootstrap.min.css\" rel=\"stylesheet\">\n";
					fullhtml += "<link href=\"file:///android_asset/bootstrap/custom.css\" rel=\"stylesheet\">\n";
					fullhtml += "</head>\n";
					fullhtml += "<body class=\"android-background\">\n";
					fullhtml += html;
					fullhtml += "</body>\n";
					fullhtml += "</html>";
					LoadDataWithBaseURL("file:///android_asset/bootstrap", fullhtml, "text/html", "UTF-8", null);
				}
			}

			/// <summary>
			/// <para>Custom WebView with Bootstrap integrated.</para>
			/// To use Bootstrap copy the following files:<br/>
			/// bootstrap.min.css => Assets/bootstrap/bootstrap.min.css<br/>
			/// custom.css => Assets/bootstrap/custom.css
			/// </summary>
			public class BootstrapWebView : WebView
			{
				/// <summary>
				/// Gets fired if the webpage is fully loaded
				/// </summary>
				public EventHandler OnLoadingDone { get { return client.OnLoadingDone; } set { client.OnLoadingDone = value; } }
				/// <summary>
				/// Gets fired when an action gots fired. eg. action://something<br/>
				/// Returns an ActionFiredArgs EventArgument.
				/// </summary>
				public EventHandler<WebViewClients.ActionFiredArgs> OnActionFired { get { return client.OnActionFired; } set { client.OnActionFired = value; } }

				private WebViewClients.WebClientPlus client;

				private List<string> scripts = new List<string>();

				public BootstrapWebView(Context context) : base(context)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapWebView(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapWebView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapWebView(Context context, Android.Util.IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
				{
					StartUp();
					CreateClient(Platform.CurrentActivity);
				}

				public BootstrapWebView(Activity activity) : base(activity.ApplicationContext)
				{
					CreateClient(activity);
					StartUp();
				}

				public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
				{
					if(keyCode == Keycode.Back && CanGoBack())
					{
						GoBack();
						return true;
					}
					return base.OnKeyDown(keyCode, e);
				}

				private void StartUp()
				{
					Settings.JavaScriptEnabled = true;
					Settings.DomStorageEnabled = true;
					Settings.AllowFileAccess = true;
					Settings.AllowFileAccessFromFileURLs = true;
					Settings.BuiltInZoomControls = true;
				}

				/// <summary>
				/// Creates the WebClientPlus
				/// </summary>
				/// <param name="activity">The activity it's called from</param>
				public void CreateClient(Activity activity)
				{
					client = new WebViewClients.WebClientPlus(activity);
					SetWebViewClient(client);
				}

				/// <summary>
				/// Add a simple action which calls an Intent or read it with the OnActionFired Eventhandler
				/// </summary>
				/// <param name="key">The key of the action (a link would look like key://action)</param>
				/// <param name="intent">The intent which should be called when the action gets fired</param>
				public void AddAction(string key, Intent intent = null)
				{
					client.AddAction(key, intent);
				}

				/// <summary>
				/// Removes the action
				/// </summary>
				/// <param name="key">The key of the action</param>
				public void RemoveAction(string key)
				{
					client.RemoveAction(key);
				}

				/// <summary>
				/// Adds a script from the Assets folder
				/// </summary>
				/// <param name="assetdir">The file, relative to the Assets folder</param>
				public void AddLocalScript(string assetdir)
				{
					string line = "<script src=\"file:///android_asset/" + assetdir + "\"></script>";
					scripts.Add(line);
				}

				/// <summary>
				/// Adds a remote script from an Url<br />
				/// Advice: Use assets scripts to guarantee usability even without an internet connection!
				/// </summary>
				/// <param name="url">The Url from the script</param>
				public void AddScript(string url)
				{
					string line = "<script src=\"" + url  + "\"></script>";
					scripts.Add(line);
				}

				/// <summary>
				/// Adds a script with the body content. Great for custom scripts on the fly
				/// </summary>
				/// <param name="body">Everything inside the "script"-tags</param>
				public void WriteScript(string body)
				{
					string line = "<script>\n" + body + "\n</script>";
					scripts.Add(line);
				}

				/// <summary>
				/// Removes ALL scripts from the WebView
				/// </summary>
				public void ClearScripts()
				{
					scripts = new List<string>();
				}

				/// <summary>
				/// Shows a html page from the Assets folder and replaces strings with the replacements keyvaluepairs.
				/// </summary>
				/// <param name="assetloc">The file location relative to the Assets folder</param>
				/// <param name="replacements">A dictionary of strings that will be replaced by strings</param>
				public void ShowAsset(string assetloc, Dictionary<string,string> replacements)
				{
					AssetManager manager = client.GetActivity().Assets;
					using(StreamReader sr = new StreamReader(manager.Open(assetloc)))
					{
						string content = sr.ReadToEnd();
						foreach(KeyValuePair<string,string> replacement in replacements)
						{
							content = content.Replace(replacement.Key, replacement.Value);
						}
						Show(content);
					}
				}

				/// <summary>
				/// Shows the HTML body content
				/// </summary>
				/// <param name="html">The html body</param>
				public void Show(string html)
				{
					string fullhtml = "<!DOCTYPE html>\n";
					fullhtml += "<html>\n";
					fullhtml += "<head>\n";
					fullhtml += "<meta charset=\"utf-8\">\n";
					fullhtml += "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\n";
					fullhtml += "<link href=\"file:///android_asset/bootstrap/bootstrap.min.css\" rel=\"stylesheet\">\n";
					fullhtml += "<link href=\"file:///android_asset/bootstrap/custom.css\" rel=\"stylesheet\">\n";
					fullhtml += "</head>\n";
					fullhtml += "<body class=\"android-background\">\n";
					fullhtml += html + "\n";
					//Add scripts to the end of the body
					Log.Debug("HTML", html);
					Log.Debug("Scripts", "Amount: " + scripts.Count);
					foreach(string script in scripts)
					{
						fullhtml += "\n" + script + "\n";
					}
					fullhtml += "</body>\n";
					fullhtml += "</html>";
					Log.Debug("Full HTML", fullhtml);
					LoadDataWithBaseURL("file:///android_asset/bootstrap", fullhtml, "text/html", "UTF-8", null);
				}

				/// <summary>
				/// Add a new JavascriptInterface (See Utils.JSInterface as an example)
				/// </summary>
				/// <param name="name">The name of the interface (will be called like that in JavaScript</param>
				/// <param name="obj">The Java.Lang.Object</param>
				public void AddJSInterface(string name, Java.Lang.Object obj)
				{
					AddJavascriptInterface(obj, name);
				}

				/// <summary>
				/// Remove an existing JavascriptInterface
				/// </summary>
				/// <param name="name">The name of the JavascriptInterface</param>
				public void RemoveJSInterface(string name)
				{
					RemoveJavascriptInterface(name);
				}
			}
		}

		namespace WebViewClients
		{
			/// <summary>
			/// <para>Custom WebViewClient with OnLoadingDone and OnActionFired EventHandlers.</para>
			/// Can be used to handle PageLoading events or events when certain actions are fired.<br />
			/// Actions can be set as a string=>Intent pair
			/// </summary>
			public class WebClientPlus : WebViewClient
			{
				public EventHandler OnLoadingDone;
				public EventHandler<ActionFiredArgs> OnActionFired;

				private readonly Activity activity;
				public Activity GetActivity()
				{
					return activity;
				}

				private Dictionary<string, Intent> actions = new Dictionary<string, Intent>();

				/// <summary>
				/// <para>Custom WebViewClient with OnLoadingDone and OnActionFired EventHandlers.</para>
				/// Can be used to handle PageLoading events or events when certain actions are fired.<br />
				/// Actions can be set as a string=>Intent pair
				/// </summary>
				/// <param name="activity">The main activity to handle Intent calls</param>
				public WebClientPlus(Activity activity)
				{
					this.activity = activity;
				}

				/// <summary>
				/// <para>Custom WebViewClient with OnLoadingDone and OnActionFired EventHandlers.</para>
				/// Can be used to handle PageLoading events or events when certain actions are fired.<br />
				/// Actions can be set as a string=>Intent pair
				/// </summary>
				/// <param name="activity">The main activity to handle Intent calls</param>
				/// <param name="actions">The actions as a string=>Intent dictionary.<br />
				/// Intent won't be called if null.</param>
				public WebClientPlus(Activity activity, Dictionary<string,Intent> actions)
				{
					this.activity = activity;
					this.actions = actions;
				}

				public override void OnPageFinished(WebView view, string url)
				{
					OnLoadingDone?.Invoke(this, null);
					base.OnPageFinished(view, url);
				}

				public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
				{
					foreach(KeyValuePair<string, Intent> action in actions)
					{
						string url = request.Url.ToString();
						if (url.StartsWith(action.Key + "://"))
						{
							string paras = url.Replace(action.Key + "://", "");
							Intent intent = action.Value;
							
							if(intent != null)
							{
								intent.PutExtra("url", url);
								intent.PutExtra("paras", paras);
							}

							OnActionFired?.Invoke(this, new ActionFiredArgs()
							{
								url = url,
								paras = paras,
								intent = intent
							});

							if(activity != null && intent != null)
							{
								activity.StartActivity(intent);
							}

							return true;
						}
					}
					return base.ShouldOverrideUrlLoading(view, request);
				}

				public void AddAction(string key, Intent intent = null)
				{
					if(actions.ContainsKey(key))
					{
						throw new Exception("Action " + key + " already exists!");
					}
					actions.Add(key, intent);
				}

				public void RemoveAction(string key)
				{
					actions.Remove(key);
				}
			}

			public class ActionFiredArgs : EventArgs
			{
				public string url;
				public string paras;
				public Intent intent;
			}
		}

		namespace Utils
		{
			/// <summary>
			/// Interface for JavaScript<br />
			/// Work in Progress!
			/// </summary>
			public class JSInterface : Java.Lang.Object
			{
				Context context;

				public JSInterface(Context context)
				{
					this.context = context;
				}

				[Export]
				[JavascriptInterface]
				public void ShowToast(string name)
				{
					Toast.MakeText(context, "Hello from " + name, ToastLength.Short).Show();
				}

				[Export]
				[JavascriptInterface]
				public string ShowText()
				{
					return "Belgien";
				}
			}
		}
	}
}