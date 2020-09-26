// Decompiled with JetBrains decompiler
// Type: Analogy.LogViewer.RabbitMq.Properties.Resources
// Assembly: Analogy.LogViewer.RabbitMq, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E0FDF71-059B-43BA-8298-9C79EA8D7A83
// Assembly location: C:\Users\liorb\Downloads\Analogy.LogViewer.RabbitMq.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Analogy.LogViewer.RabbitMq.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (resourceMan == null)
          resourceMan = new ResourceManager("Analogy.LogViewer.RabbitMq.Properties.Resources", typeof (Resources).Assembly);
        return resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => resourceCulture;
      set => resourceCulture = value;
    }

    internal static Bitmap rabbit_16x16 => (Bitmap) ResourceManager.GetObject(nameof (rabbit_16x16), resourceCulture);

    internal static Bitmap rabbit_32x32 => (Bitmap) ResourceManager.GetObject(nameof (rabbit_32x32), resourceCulture);
  }
}
