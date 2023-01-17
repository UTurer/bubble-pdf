Introduction
Pdfium.NET SDK it's a class library based on the PDFium project for viewing, navigating, editing and extracting texts from PDF files in your .NET projects.

Files included in package:
 • netXX\Patagames.Pdf.dll - Main assembly
 • netXX\Patagames.Pdf.xml - Xml comments for main assembly
 • netXX\Patagames.Pdf.WinForms.dll - Assembly what contains PdfViewer control for WinForms
 • netXX\Patagames.Pdf.WinForms.xml - Xml comments for PdfViewer control
 • netXX\Patagames.Pdf.Wpf.dll - Assembly what contains PdfViewer control for WPF
 • netXX\Patagames.Pdf.Wpf.xml - Xml comments for PdfViewer control
 • uap10.0\Patagames.Pdf.dll - Main assemvly targeted to Universal Windows Platform (uap 10.0; Build 10240 - 14393) - beta version
 • uap10.0\Patagames.Pdf.xml - Xml comments for main assembly
 • uap10.0\Beta Notice.txt - beta notice
 • netstandard20\Patagames.Pdf.dll - Main assembly targeted to .Net Standard 2.0
 • netstandard20\Patagames.Pdf.xml - Xml comments for main assembly
 • netstandard20\Patagames.Pdf.deps.json - dependency file
 • netstandard20\Beta Notice.txt - beta notice
 • netstandard21\Patagames.Pdf.dll - Main assembly targeted to .Net Standard 2.1
 • netstandard21\Patagames.Pdf.xml - Xml comments for main assembly
 • netstandard21\Patagames.Pdf.deps.json - dependency file
 • netstandard21\Beta Notice.txt - beta notice
 • x64\pdfium.dll - the 64-bit version of the Pdfium library;
 • x86\pdfium.dll - the 32-bit version of the Pdfium library;
 • icudt.dll - International Components for Unicode data file - provides Unicode and Globalization support for acro forms;
 • Sources\*.* - the source codes for PdfViewer controls (WPF and WinForms)
 • Pdf.Net.SDK.licx -.Net licensing file;
 • Pdf.Net SDK Activation - Activation tool;
 • readme.txt - this file

Compatibility
Pdfium.Net SDK is available for .Net Framework 2.0 - 4.8, .Net Standard 2.0 - 2.1 and uap10.0 on 32 and 64-bit operating systems.
SDK has been tested with Windows XP, Vista, 7, 8, 8.1 and 10, and is fully compatible with all of them. The native PDFium.dll library included in this project is a 32-bit and 64-bit version, so your .NET application can be "Any CPU".


Getting Started
Pdfium.NET SDK provides a number of components to work with PDF files:
 • Patagames.Pdf.dll contains the class library used to load, manipulate and render PDF documents;
 • Patagames.Pdf.WinForms.dll contains a WinForms controls that can render a PdfDocument (Follow the steps on this page to add PdfViewer control into Toolbox);
 • Patagames.Pdf.Wpf.dll contains a Wpf controls that can render a PdfDocument;
 • Pdfium.dll it's a native pdfium engine;

1. To use the library, you must first add a reference to Patagames.Pdf.dll and/or Patagames.Pdf.WinForms.dll or Patagames.Pdf.Wpf.dll files into your project.
2. After you've added this reference, you need add two files to your project:
 • x86\pdfium.dll is the 32-bit version of the Pdfium library;
 • x64\pdfium.dll is the 64-bit version of the Pdfium library;

Note
You have two options. If your application is 32-bit only or 64-bit only, you can remove the DLL that won't be used. You can leave this file in the x86 or x64 directory, or move it to the root of your project. Library will find the DLL in both cases.

3. When building your project, the pdfium.dll library(s) must be placed next to your application, either in the root or the x86 or x64 sub directory. The easiest way to accomplish this is by changing the properties of that file, changing the Copy to Output Directory setting to Copy always.

That's all. Your project is ready for using Pdfium.Net SDK

Important
You must call PdfCommon.Initialize() function at some point before using any PDF processing functions. 
If you have problems with initialization (if it says something like "Unable to load DLL 'pdfium.dll'"), try to specify the full path to Pdfium.dll through the specificPath parameter. Read here for more details.

Bugs
Bugs should be reported through email at support@patagames.com