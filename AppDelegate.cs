// AppDelegate.cs: Customize (add tests assemblies) your runner application here!
//
// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2011-2013 Xamarin Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System.Reflection;
using MonoTouch.NUnit.UI;
using UIKit;
using Foundation;

namespace MonoTouch.NUnit {

	/// <summary>
	/// The UIApplicationDelegate for the application. This class is responsible for launching the 
	/// User Interface of the application, as well as listening (and optionally responding) to 
	/// application events from iOS.
	/// </summary>
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {
		// class-level declarations
		UIWindow window;
		TouchRunner runner;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			runner = new TouchRunner (window);

			// tests can be inside the main assembly
			runner.Add (Assembly.GetExecutingAssembly ());
#if false
			// you can use the default or set your own custom writer (e.g. save to web site and tweet it ;-)
			runner.Writer = new TcpTextWriter ("10.0.1.2", 16384);
			// start running the test suites as soon as the application is loaded
			runner.AutoStart = true;
			// crash the application (to ensure it's ended) and return to springboard
			runner.TerminateAfterExecution = true;
#endif
#if false
			// you can get NUnit[2-3]-style XML reports to the console or server like this
			// replace `null` (default to Console.Out) to a TcpTextWriter to send data to a socket server
			// replace `NUnit2XmlOutputWriter` with `NUnit3XmlOutputWriter` for NUnit3 format
			runner.Writer = new NUnitOutputTextWriter (runner, null, new NUnitLite.Runner.NUnit2XmlOutputWriter ());
			// the same AutoStart and TerminateAfterExecution can be used for build automation
#endif
			window.RootViewController = new UINavigationController (runner.GetViewController ());
			window.MakeKeyAndVisible ();
			return true;
		}
	}
}