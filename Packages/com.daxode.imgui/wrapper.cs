#define IMGUI_USE_WCHAR32
#define IMGUI_DISABLE_OBSOLETE_KEYIO
#define IMGUI_DISABLE_OBSOLETE_FUNCTIONS

using System;
using System.Runtime.InteropServices;
using com.daxode.imgui;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
#pragma warning disable CS0414 // Field is assigned but its value is never used

static class ImGui
{
	public const string VERSION = "1.90.3";
	const int VERSION_NUM = 19030;
	const bool HAS_TABLE = true;
	
    
	[DllImport("cimgui", EntryPoint = "igGetCurrentContext")]
	public static extern unsafe ImGuiContext* GetCurrentContext();
	
	[DllImport("cimgui", EntryPoint = "igDestroyContext")]
	public static extern void DestroyContext();

	[DllImport("cimgui", EntryPoint = "igGetDrawData")]
	public static extern unsafe ImDrawData* GetDrawData();
	
	[DllImport("cimgui", EntryPoint = "igRender")]
	public static extern void Render();

	[DllImport("cimgui", EntryPoint = "igEnd")]
	public static extern void End();
	
	[DllImport("cimgui")]
	static extern unsafe void igShowDemoWindow(byte* p_open);

	public static unsafe void ShowDemoWindow(ref bool p_open)
		=> igShowDemoWindow((byte*)UnsafeUtility.AddressOf(ref p_open));
	
	[DllImport("cimgui")]
	static extern unsafe ImGuiID igDockSpace(ImGuiID id, float2 size,ImGuiDockNodeFlags flags, ImGuiWindowClass* windowClass);
	public static unsafe ImGuiID DockSpace(ImGuiID id, float2 size = default, ImGuiDockNodeFlags flags = 0, ImGuiWindowClass* windowClass = null)
		=> igDockSpace(id, size, flags, windowClass);
	
	[DllImport("cimgui")]
	static extern unsafe ImGuiID igGetID_Str(byte* strId);                                      
	/// calculate unique ID (hash of whole ID stack + given parameter). e.g. if you want to query into ImGuiStorage yourself
	public static unsafe ImGuiID GetID(FixedString128Bytes strId) 
		=> igGetID_Str(strId.GetUnsafePtr());
	
    
	[DllImport("cimgui", EntryPoint = "igPushItemWidth")]
	public static extern void          PushItemWidth(float item_width);                                // push width of items for common large "item+label" widgets. >0.0f: width in pixels, <0.0f align xx pixels to the right of window (so -FLT_MIN always align width to the right side).
	[DllImport("cimgui", EntryPoint = "igPopItemWidth")]
	public static extern void          PopItemWidth();
	
	public static unsafe void Begin(FixedString128Bytes anotherWindow, ImGuiWindowFlags flags = 0) 
		=> Begin(anotherWindow.GetUnsafePtr(), null, flags);
	public static unsafe void Begin(FixedString128Bytes anotherWindow, ref bool b, ImGuiWindowFlags flags = 0) 
		=> Begin(anotherWindow.GetUnsafePtr(), UnsafeUtility.AddressOf(ref b), flags);
	public static unsafe void Begin(NativeText anotherWindow, ref bool b, ImGuiWindowFlags flags = 0) 
		=> Begin((byte*)anotherWindow.GetUnsafePtr(), UnsafeUtility.AddressOf(ref b), flags);
	[DllImport("cimgui", EntryPoint = "igBegin")]
	static extern unsafe bool Begin(byte* name, void* p_open = null, ImGuiWindowFlags flags = 0);

	[DllImport("cimgui")]
	static extern void igImage(ImTextureID user_texture_id, float2 image_size, 
		float2 uv0, float2 uv1, 
		float4 tint_col, float4 border_col);
	public static void Image(UnityObjRef<Texture2D> user_texture_id, float2 image_size, 
		float2 uv0 = default) {
		igImage(UnsafeUtility.As<UnityObjRef<Texture2D>, ImTextureID>(ref user_texture_id), image_size, 
			uv0, new float2(1, 1), new float4(1, 1, 1, 1), default);
	}
    
	public static void Image(UnityObjRef<Texture2D> user_texture_id, float2 image_size, 
		float2 uv0, float2 uv1) {
		igImage(UnsafeUtility.As<UnityObjRef<Texture2D>, ImTextureID>(ref user_texture_id), image_size, 
			uv0, uv1, new float4(1, 1, 1, 1), default);
	}
	
	public static void Image(UnityObjRef<Texture2D> user_texture_id, float2 image_size, 
		float2 uv0, float2 uv1, float4 tint_col, float4 border_col = default) {
		igImage(UnsafeUtility.As<UnityObjRef<Texture2D>, ImTextureID>(ref user_texture_id), image_size, 
			uv0, uv1, tint_col, border_col);
	}
	
	[DllImport("cimgui")]
	static extern void igSetNextWindowPos(float2 pos, ImGuiCond cond, float2 pivot);
	public static void SetNextWindowPos(float2 pos, ImGuiCond cond = 0, float2 pivot = default) 
		=> igSetNextWindowPos(pos, cond, pivot);

	public static unsafe void Text(FixedString128Bytes text) => Text(text.GetUnsafePtr());
	public static unsafe void Text(NativeText text) => Text(text.GetUnsafePtr());
	[DllImport("cimgui", EntryPoint = "igText")]
	static extern unsafe void Text(byte* text);

	public static unsafe bool Checkbox(FixedString128Bytes demoWindow, ref bool p1)
	=> Checkbox(demoWindow.GetUnsafePtr(), UnsafeUtility.AddressOf(ref p1)) > 0;
	[DllImport("cimgui", EntryPoint = "igCheckbox")]
	static extern unsafe byte Checkbox(byte* label, void* v);

	public static unsafe bool SliderFloat(FixedString128Bytes f, ref float currentVal, float from, float to) 
		=> SliderFloat(f.GetUnsafePtr(), (float*) UnsafeUtility.AddressOf(ref currentVal), from, to, new FixedString128Bytes("%.3g").GetUnsafePtr(), 0) > 0;

	[DllImport("cimgui", EntryPoint = "igSliderFloat")]
	static extern unsafe byte SliderFloat(byte* label, float* v, float v_min, float v_max, byte* format, ImGuiSliderFlags flags = 0);     // adjust format to decorate the value with a prefix or a suffix for in-slider labels or unit display.
	
	public static unsafe bool ColorEdit3(FixedString128Bytes clearColor, ref float4 f, ImGuiColorEditFlags flags = 0) 
		=> ColorEdit3(clearColor.GetUnsafePtr(), (float*) UnsafeUtility.AddressOf(ref f), flags);
	public static unsafe bool ColorEdit3(FixedString128Bytes clearColor, ref Color f, ImGuiColorEditFlags flags = 0) 
		=> ColorEdit3(clearColor.GetUnsafePtr(), (float*) UnsafeUtility.AddressOf(ref f), flags);
	public static unsafe bool ColorEdit3(FixedString128Bytes clearColor, ref float3 f, ImGuiColorEditFlags flags = 0) 
		=> ColorEdit3(clearColor.GetUnsafePtr(), (float*) UnsafeUtility.AddressOf(ref f), flags);
	[DllImport("cimgui", EntryPoint = "igColorEdit3")]
	static extern unsafe bool ColorEdit3(byte* label, float* col, ImGuiColorEditFlags flags = 0);
	
	public static unsafe bool ColorEdit4(FixedString128Bytes clearColor, ref float4 f, ImGuiColorEditFlags flags = 0) 
		=> ColorEdit4(clearColor.GetUnsafePtr(), (float*) UnsafeUtility.AddressOf(ref f), flags);
	public static unsafe bool ColorEdit4(FixedString128Bytes clearColor, ref Color f, ImGuiColorEditFlags flags = 0) 
		=> ColorEdit4(clearColor.GetUnsafePtr(), (float*) UnsafeUtility.AddressOf(ref f), flags);
	[DllImport("cimgui", EntryPoint = "igColorEdit4")]
	static extern unsafe bool ColorEdit4(byte* label, float* col, ImGuiColorEditFlags flags = 0);

	public static unsafe bool Button(FixedString128Bytes button, float2 size = default) 
		=> Button(button.GetUnsafePtr(), size);
	[DllImport("cimgui", EntryPoint = "igButton")]
	static extern unsafe bool Button(byte* label, float2 size);   // button
	
	[DllImport("cimgui", EntryPoint = "igSameLine")]
	public static extern void SameLine(float offset_from_start_x=0.0f, float spacing=-1.0f);

	[DllImport("cimgui", EntryPoint = "igGetIO")]
	static extern unsafe ImGuiIO* igGetIO();
	public static unsafe ImGuiIO* GetIO() => igGetIO();

	[DllImport("cimgui", EntryPoint = "igStyleColorsDark")]
	public static extern unsafe void StyleColorsDark(out ImGuiStyle dst);

	[DllImport("cimgui", EntryPoint = "igNewFrame")]
	public static extern void NewFrame();

	[DllImport("cimgui", EntryPoint = "igCreateContext")]
	public static extern unsafe ImGuiContext* CreateContext(void* shared_font_atlas = null); // ImFontAtlas

	[DllImport("cimgui")]
	static extern void igSetNextWindowBgAlpha(float alpha);
	public static void SetNextWindowBgAlpha(float alpha) => igSetNextWindowBgAlpha(alpha);
	
	static unsafe bool DebugCheckVersionAndDataLayout(FixedString128Bytes version_str, int sz_io, int sz_style, int sz_vec2, int sz_vec4, int sz_drawvert, int sz_drawidx)
		=> DebugCheckVersionAndDataLayout(version_str.GetUnsafePtr(), sz_io, sz_style, sz_vec2, sz_vec4, sz_drawvert, sz_drawidx);
	
	[DllImport("cimgui", EntryPoint = "igDebugCheckVersionAndDataLayout")]
	static extern unsafe bool DebugCheckVersionAndDataLayout(byte* version_str, int sz_io, int sz_style, int sz_vec2, int sz_vec4, int sz_drawvert, int sz_drawidx); // This is called by IMGUI_CHECKVERSION() macro.
	
	
	[DllImport("cimgui")]
	static extern void          igPushStyleColor_U32(ImGuiCol idx, Color32 col);
	/// modify a style color. always use this if you modify the style after NewFrame().
	public static void PushStyleColor(ImGuiCol idx, Color32 col) => igPushStyleColor_U32(idx, col);
	
	[DllImport("cimgui")]
	static extern void          igPushStyleColor_Vec4(ImGuiCol idx, float4 col);
	public static void PushStyleColor(ImGuiCol idx, float4 col) => igPushStyleColor_Vec4(idx, col);
	public static void PushStyleColor(ImGuiCol idx, Color col) => igPushStyleColor_Vec4(idx, UnsafeUtility.As<Color, float4>(ref col));
	
	[DllImport("cimgui")]
	static extern void          igPopStyleColor(int count = 1);
	public static void PopStyleColor(int count = 1) => igPopStyleColor(count);
	
	[DllImport("cimgui")]
	static extern unsafe void          igPushStyleVar_Float(ImGuiStyleVar idx, float val);
	/// modify a style float variable. always use this if you modify the style after NewFrame().
	public static void PushStyleVar(ImGuiStyleVar idx, float val) => igPushStyleVar_Float(idx, val);
	
    [DllImport("cimgui")]
	static extern unsafe void          igPushStyleVar_Vec2(ImGuiStyleVar idx, float2 val);             
	/// modify a style ImVec2 variable. always use this if you modify the style after NewFrame().
	public static void PushStyleVar(ImGuiStyleVar idx, float2 val) => igPushStyleVar_Vec2(idx, val);

	[DllImport("cimgui")]
	static extern unsafe void igPopStyleVar(int count = 1);             
	/// Pop `count` style variable changes.
	public static void PopStyleVar(int count = 1) => igPopStyleVar(count);
	
	[DllImport("cimgui", EntryPoint = "igSetNextWindowSize")]
	public static extern unsafe void          SetNextWindowSize(float2 size, ImGuiCond cond = 0);                  // set next window size. set axis to 0.0f to force an auto-fit on this axis. call before Begin()
	
	public static unsafe void CheckVersion()
		=> DebugCheckVersionAndDataLayout(VERSION, 
			sizeof(ImGuiIO), sizeof(ImGuiStyle), sizeof(float2),
			sizeof(float4), sizeof(ImDrawVert), sizeof(ImDrawIdx));
}

enum ImGuiMouseButton : int
{
	Left = 0,
	Right = 1,
	Middle = 2,
	COUNT = 5
};

struct ImGuiWindowClass
{
	ImGuiID ClassId;
	ImGuiID ParentViewportId;
	ImGuiID FocusRouteParentWindowId;
	ImGuiViewportFlags ViewportFlagsOverrideSet;
	ImGuiViewportFlags ViewportFlagsOverrideClear;
	ImGuiTabItemFlags TabItemFlagsOverrideSet;
	ImGuiDockNodeFlags DockNodeFlagsOverrideSet;
	byte dockingAlwaysTabBar;
	bool DockingAlwaysTabBar => dockingAlwaysTabBar > 0;
	byte dockingAllowUnclassed;
	bool DockingAllowUnclassed => dockingAllowUnclassed > 0;
};

[Flags]
enum ImGuiViewportFlags
{
    None                     = 0,
    IsPlatformWindow         = 1 << 0,   // Represent a Platform Window
    IsPlatformMonitor        = 1 << 1,   // Represent a Platform Monitor (unused yet)
    OwnedByApp               = 1 << 2,   // Platform Window: Was created/managed by the user application? (rather than our backend)
    NoDecoration             = 1 << 3,   // Platform Window: Disable platform decorations: title bar, borders, etc. (generally set all windows, but if ImGuiConfigFlags_ViewportsDecoration is set we only set this on popups/tooltips)
    NoTaskBarIcon            = 1 << 4,   // Platform Window: Disable platform task bar icon (generally set on popups/tooltips, or all windows if ImGuiConfigFlags_ViewportsNoTaskBarIcon is set)
    NoFocusOnAppearing       = 1 << 5,   // Platform Window: Don't take focus when created.
    NoFocusOnClick           = 1 << 6,   // Platform Window: Don't take focus when clicked on.
    NoInputs                 = 1 << 7,   // Platform Window: Make mouse pass through so we can drag this window while peaking behind it.
    NoRendererClear          = 1 << 8,   // Platform Window: Renderer doesn't need to clear the framebuffer ahead (because we will fill it entirely).
    NoAutoMerge              = 1 << 9,   // Platform Window: Avoid merging this window into another host window. This can only be set via ImGuiWindowClass viewport flags override (because we need to now ahead if we are going to create a viewport in the first place!).
    TopMost                  = 1 << 10,  // Platform Window: Display on top (for tooltips only).
    CanHostOtherWindows      = 1 << 11,  // Viewport can host multiple imgui windows (secondary viewports are associated to a single window). // FIXME: In practice there's still probably code making the assumption that this is always and only on the MainViewport. Will fix once we add support for "no main viewport".

    // Output status flags (from Platform)
    IsMinimized              = 1 << 12,  // Platform Window: Window is minimized, can skip render. When minimized we tend to avoid using the viewport pos/size for clipping window or testing if they are contained in the viewport.
    IsFocused                = 1 << 13,  // Platform Window: Window is focused (last call to Platform_GetWindowFocus() returned true)
}

[Flags]
enum ImGuiDockNodeFlags {
	None = 0,
	KeepAliveOnly = 1 << 0,
	NoDockingOverCentralNode = 1 << 2,
	PassthruCentralNode = 1 << 3,
	NoDockingSplit = 1 << 4,
	NoResize = 1 << 5,
	AutoHideTabBar = 1 << 6,
	NoUndocking = 1 << 7,
}

[Flags]
enum ImGuiTabItemFlags {
	None = 0,
	UnsavedDocument = 1 << 0,
	SetSelected = 1 << 1,
	NoCloseWithMiddleMouseButton = 1 << 2,
	NoPushId = 1 << 3,
	NoTooltip = 1 << 4,
	NoReorder = 1 << 5,
	Leading = 1 << 6,
	Trailing = 1 << 7,
	NoAssumedClosure = 1 << 8,
}


// Enumeration for ImGui::SetNextWindow***(), SetWindow***(), SetNextItem***() functions
// Represent a condition.
// Important: Treat as a regular enum! Do NOT combine multiple values using binary operators! All the functions above treat 0 as a shortcut to ImGuiCond_Always.
enum ImGuiCond
{
	None          = 0,        // No condition (always set the variable), same as _Always
	Always        = 1 << 0,   // No condition (always set the variable), same as _None
	Once          = 1 << 1,   // Set the variable once per runtime session (only the first call will succeed)
	FirstUseEver  = 1 << 2,   // Set the variable if the object/window has no persistently saved data (no entry in .ini file)
	Appearing     = 1 << 3,   // Set the variable if the object/window is appearing after being hidden/inactive (or the first time)
};

// Enumeration for PushStyleVar() / PopStyleVar() to temporarily modify the ImGuiStyle structure.
// - The enum only refers to fields of ImGuiStyle which makes sense to be pushed/popped inside UI code.
//   During initialization or between frames, feel free to just poke into ImGuiStyle directly.
// - Tip: Use your programming IDE navigation facilities on the names in the _second column_ below to find the actual members and their description.
//   In Visual Studio IDE: CTRL+comma ("Edit.GoToAll") can follow symbols in comments, whereas CTRL+F12 ("Edit.GoToImplementation") cannot.
//   With Visual Assist installed: ALT+G ("VAssistX.GoToImplementation") can also follow symbols in comments.
// - When changing this enum, you need to update the associated internal table GStyleVarInfo[] accordingly. This is where we link enum values to members offset/type.
enum ImGuiStyleVar
{
    // Enum name --------------------- // Member in ImGuiStyle structure (see ImGuiStyle for descriptions)
    Alpha,               // float     Alpha
    DisabledAlpha,       // float     DisabledAlpha
    WindowPadding,       // ImVec2    WindowPadding
    WindowRounding,      // float     WindowRounding
    WindowBorderSize,    // float     WindowBorderSize
    WindowMinSize,       // ImVec2    WindowMinSize
    WindowTitleAlign,    // ImVec2    WindowTitleAlign
    ChildRounding,       // float     ChildRounding
    ChildBorderSize,     // float     ChildBorderSize
    PopupRounding,       // float     PopupRounding
    PopupBorderSize,     // float     PopupBorderSize
    FramePadding,        // ImVec2    FramePadding
    FrameRounding,       // float     FrameRounding
    FrameBorderSize,     // float     FrameBorderSize
    ItemSpacing,         // ImVec2    ItemSpacing
    ItemInnerSpacing,    // ImVec2    ItemInnerSpacing
    IndentSpacing,       // float     IndentSpacing
    CellPadding,         // ImVec2    CellPadding
    ScrollbarSize,       // float     ScrollbarSize
    ScrollbarRounding,   // float     ScrollbarRounding
    GrabMinSize,         // float     GrabMinSize
    GrabRounding,        // float     GrabRounding
    TabRounding,         // float     TabRounding
    TabBarBorderSize,    // float     TabBarBorderSize
    ButtonTextAlign,     // ImVec2    ButtonTextAlign
    SelectableTextAlign, // ImVec2    SelectableTextAlign
    SeparatorTextBorderSize,// float  SeparatorTextBorderSize
    SeparatorTextAlign,  // ImVec2    SeparatorTextAlign
    SeparatorTextPadding,// ImVec2    SeparatorTextPadding
    DockingSeparatorSize,// float     DockingSeparatorSize
    COUNT
};


// Flags for ColorEdit3() / ColorEdit4() / ColorPicker3() / ColorPicker4() / ColorButton()
[Flags]
enum ImGuiColorEditFlags
{
    None            = 0,
    NoAlpha         = 1 << 1,   //              // ColorEdit, ColorPicker, ColorButton: ignore Alpha component (will only read 3 components from the input pointer).
    NoPicker        = 1 << 2,   //              // ColorEdit: disable picker when clicking on color square.
    NoOptions       = 1 << 3,   //              // ColorEdit: disable toggling options menu when right-clicking on inputs/small preview.
    NoSmallPreview  = 1 << 4,   //              // ColorEdit, ColorPicker: disable color square preview next to the inputs. (e.g. to show only the inputs)
    NoInputs        = 1 << 5,   //              // ColorEdit, ColorPicker: disable inputs sliders/text widgets (e.g. to show only the small preview color square).
    NoTooltip       = 1 << 6,   //              // ColorEdit, ColorPicker, ColorButton: disable tooltip when hovering the preview.
    NoLabel         = 1 << 7,   //              // ColorEdit, ColorPicker: disable display of inline text label (the label is still forwarded to the tooltip and picker).
    NoSidePreview   = 1 << 8,   //              // ColorPicker: disable bigger color preview on right side of the picker, use small color square preview instead.
    NoDragDrop      = 1 << 9,   //              // ColorEdit: disable drag and drop target. ColorButton: disable drag and drop source.
    NoBorder        = 1 << 10,  //              // ColorButton: disable border (which is enforced by default)

    // User Options (right-click on widget to change some of them).
    AlphaBar        = 1 << 16,  //              // ColorEdit, ColorPicker: show vertical alpha bar/gradient in picker.
    AlphaPreview    = 1 << 17,  //              // ColorEdit, ColorPicker, ColorButton: display preview as a transparent color over a checkerboard, instead of opaque.
    AlphaPreviewHalf= 1 << 18,  //              // ColorEdit, ColorPicker, ColorButton: display half opaque / half checkerboard, instead of opaque.
    HDR             = 1 << 19,  //              // (WIP) ColorEdit: Currently only disable 0.0f..1.0f limits in RGBA edition (note: you probably want to use Float flag as well).
    DisplayRGB      = 1 << 20,  // [Display]    // ColorEdit: override _display_ type among RGB/HSV/Hex. ColorPicker: select any combination using one or more of RGB/HSV/Hex.
    DisplayHSV      = 1 << 21,  // [Display]    // "
    DisplayHex      = 1 << 22,  // [Display]    // "
    Uint8           = 1 << 23,  // [DataType]   // ColorEdit, ColorPicker, ColorButton: _display_ values formatted as 0..255.
    Float           = 1 << 24,  // [DataType]   // ColorEdit, ColorPicker, ColorButton: _display_ values formatted as 0.0f..1.0f floats instead of 0..255 integers. No round-trip of value via integers.
    PickerHueBar    = 1 << 25,  // [Picker]     // ColorPicker: bar for Hue, rectangle for Sat/Value.
    PickerHueWheel  = 1 << 26,  // [Picker]     // ColorPicker: wheel for Hue, triangle for Sat/Value.
    InputRGB        = 1 << 27,  // [Input]      // ColorEdit, ColorPicker: input and output data in RGB format.
    InputHSV        = 1 << 28,  // [Input]      // ColorEdit, ColorPicker: input and output data in HSV format.

    // Defaults Options. You can set application defaults using SetColorEditOptions(). The intent is that you probably don't want to
    // override them in most of your calls. Let the user choose via the option menu and/or call SetColorEditOptions() once during startup.
    DefaultOptions_ = Uint8 | DisplayRGB | InputRGB | PickerHueBar,

    // [Internal] Masks
    DisplayMask_    = DisplayRGB | DisplayHSV | DisplayHex,
    DataTypeMask_   = Uint8 | Float,
    PickerMask_     = PickerHueWheel | PickerHueBar,
    InputMask_      = InputRGB | InputHSV,

    // Obsolete names
    //RGB = DisplayRGB, HSV = DisplayHSV, HEX = DisplayHex  // [renamed in 1.69]
};

[Flags]
enum ImGuiSliderFlags
{
	None                   = 0,
	AlwaysClamp            = 1 << 4,       // Clamp value to min/max bounds when input manually with CTRL+Click. By default CTRL+Click allows going out of bounds.
	Logarithmic            = 1 << 5,       // Make the widget logarithmic (linear otherwise). Consider using NoRoundToFormat with this if using a format-string with small amount of digits.
	NoRoundToFormat        = 1 << 6,       // Disable rounding underlying value to match precision of the display format string (e.g. %.3f values are rounded to those 3 digits)
	NoInput                = 1 << 7,       // Disable CTRL+Click or Enter key allowing to input text directly into the widget
	InvalidMask_           = 0x7000000F,   // [Internal] We treat using those bits as being potentially a 'float power' argument from the previous API that has got miscast to this enum, and will trigger an assert if needed.

	// Obsolete names
	//ClampOnInput = AlwaysClamp, // [renamed in 1.79]
};

// Flags for ImGui::Begin()
// (Those are per-window flags. There are shared flags in ImGuiIO: io.ConfigWindowsResizeFromEdges and io.ConfigWindowsMoveFromTitleBarOnly)
[Flags]
enum ImGuiWindowFlags
{
    None                   = 0,
    NoTitleBar             = 1 << 0,   // Disable title-bar
    NoResize               = 1 << 1,   // Disable user resizing with the lower-right grip
    NoMove                 = 1 << 2,   // Disable user moving the window
    NoScrollbar            = 1 << 3,   // Disable scrollbars (window can still scroll with mouse or programmatically)
    NoScrollWithMouse      = 1 << 4,   // Disable user vertically scrolling with mouse wheel. On child window, mouse wheel will be forwarded to the parent unless NoScrollbar is also set.
    NoCollapse             = 1 << 5,   // Disable user collapsing window by double-clicking on it. Also referred to as Window Menu Button (e.g. within a docking node).
    AlwaysAutoResize       = 1 << 6,   // Resize every window to its content every frame
    NoBackground           = 1 << 7,   // Disable drawing background color (WindowBg, etc.) and outside border. Similar as using SetNextWindowBgAlpha(0.0f).
    NoSavedSettings        = 1 << 8,   // Never load/save settings in .ini file
    NoMouseInputs          = 1 << 9,   // Disable catching mouse, hovering test with pass through.
    MenuBar                = 1 << 10,  // Has a menu-bar
    HorizontalScrollbar    = 1 << 11,  // Allow horizontal scrollbar to appear (off by default). You may use SetNextWindowContentSize(ImVec2(width,0.0f)); prior to calling Begin() to specify width. Read code in imgui_demo in the "Horizontal Scrolling" section.
    NoFocusOnAppearing     = 1 << 12,  // Disable taking focus when transitioning from hidden to visible state
    NoBringToFrontOnFocus  = 1 << 13,  // Disable bringing window to front when taking focus (e.g. clicking on it or programmatically giving it focus)
    AlwaysVerticalScrollbar= 1 << 14,  // Always show vertical scrollbar (even if ContentSize.y < Size.y)
    AlwaysHorizontalScrollbar=1<< 15,  // Always show horizontal scrollbar (even if ContentSize.x < Size.x)
    NoNavInputs            = 1 << 16,  // No gamepad/keyboard navigation within the window
    NoNavFocus             = 1 << 17,  // No focusing toward this window with gamepad/keyboard navigation (e.g. skipped by CTRL+TAB)
    UnsavedDocument        = 1 << 18,  // Display a dot next to the title. When used in a tab/docking context, tab is selected when clicking the X + closure is not assumed (will wait for user to stop submitting the tab). Otherwise closure is assumed when pressing the X, so if you keep submitting the tab may reappear at end of tab bar.
    NoNav                  = NoNavInputs | NoNavFocus,
    NoDecoration           = NoTitleBar | NoResize | NoScrollbar | NoCollapse,
    NoInputs               = NoMouseInputs | NoNavInputs | NoNavFocus,

    // [Internal]
    NavFlattened           = 1 << 23,  // [BETA] On child window: share focus scope, allow gamepad/keyboard navigation to cross over parent border to this child or between sibling child windows.
    ChildWindow            = 1 << 24,  // Don't use! For internal use by BeginChild()
    Tooltip                = 1 << 25,  // Don't use! For internal use by BeginTooltip()
    Popup                  = 1 << 26,  // Don't use! For internal use by BeginPopup()
    Modal                  = 1 << 27,  // Don't use! For internal use by BeginPopupModal()
    ChildMenu              = 1 << 28,  // Don't use! For internal use by BeginMenu()

    // Obsolete names
#if !IMGUI_DISABLE_OBSOLETE_FUNCTIONS
    AlwaysUseWindowPadding = 1 << 30,  // Obsoleted in 1.90: Use ImGuiChildFlags_AlwaysUseWindowPadding in BeginChild() call.
#endif
};

[StructLayout(LayoutKind.Sequential)]
struct ImDrawIdx
{
	public ushort Value;
}

[StructLayout(LayoutKind.Sequential)]
struct ImDrawVert
{
	public float2  pos;
	public uint  col;
	public float2  uv;
};

//-----------------------------------------------------------------------------
// [SECTION] ImGuiStyle
//-----------------------------------------------------------------------------
// You may modify the ImGui::GetStyle() main instance during initialization and before NewFrame().
// During the frame, use ImGui::PushStyleVar(XXXX)/PopStyleVar() to alter the main style values,
// and ImGui::PushStyleColor(XXX)/PopStyleColor() for colors.
//-----------------------------------------------------------------------------

enum ImGuiDir : int
{
	None    = -1,
	Left    = 0,
	Right   = 1,
	Up      = 2,
	Down    = 3,
	COUNT
};


[StructLayout(LayoutKind.Sequential)]
struct ImGuiStyle
{
	 float       Alpha;                      // Global alpha applies to everything in Dear ImGui.
    float       DisabledAlpha;              // Additional alpha multiplier applied by BeginDisabled(). Multiply over current value of Alpha.
    float2      WindowPadding;              // Padding within a window.
    float       WindowRounding;             // Radius of window corners rounding. Set to 0.0f to have rectangular windows. Large values tend to lead to variety of artifacts and are not recommended.
    float       WindowBorderSize;           // Thickness of border around windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    float2      WindowMinSize;              // Minimum window size. This is a global setting. If you want to constrain individual windows, use SetNextWindowSizeConstraints().
    float2      WindowTitleAlign;           // Alignment for title bar text. Defaults to (0.0f,0.5f) for left-aligned,vertically centered.
    ImGuiDir    WindowMenuButtonPosition;   // Side of the collapsing/docking button in the title bar (None/Left/Right). Defaults to Left.
    float       ChildRounding;              // Radius of child window corners rounding. Set to 0.0f to have rectangular windows.
    float       ChildBorderSize;            // Thickness of border around child windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    float       PopupRounding;              // Radius of popup window corners rounding. (Note that tooltip windows use WindowRounding)
    float       PopupBorderSize;            // Thickness of border around popup/tooltip windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    float2      FramePadding;               // Padding within a framed rectangle (used by most widgets).
    float       FrameRounding;              // Radius of frame corners rounding. Set to 0.0f to have rectangular frame (used by most widgets).
    float       FrameBorderSize;            // Thickness of border around frames. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    float2      ItemSpacing;                // Horizontal and vertical spacing between widgets/lines.
    float2      ItemInnerSpacing;           // Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label).
    float2      CellPadding;                // Padding within a table cell. CellPadding.y may be altered between different rows.
    float2      TouchExtraPadding;          // Expand reactive bounding box for touch-based system where touch position is not accurate enough. Unfortunately we don't sort widgets so priority on overlap will always be given to the first widget. So don't grow this too much!
    float       IndentSpacing;              // Horizontal indentation when e.g. entering a tree node. Generally == (FontSize + FramePadding.x*2).
    float       ColumnsMinSpacing;          // Minimum horizontal spacing between two columns. Preferably > (FramePadding.x + 1).
    float       ScrollbarSize;              // Width of the vertical scrollbar, Height of the horizontal scrollbar.
    float       ScrollbarRounding;          // Radius of grab corners for scrollbar.
    float       GrabMinSize;                // Minimum width/height of a grab box for slider/scrollbar.
    float       GrabRounding;               // Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.
    float       LogSliderDeadzone;          // The size in pixels of the dead-zone around zero on logarithmic sliders that cross zero.
    float       TabRounding;                // Radius of upper corners of a tab. Set to 0.0f to have rectangular tabs.
    float       TabBorderSize;              // Thickness of border around tabs.
    float       TabMinWidthForCloseButton;  // Minimum width for close button to appear on an unselected tab when hovered. Set to 0.0f to always show when hovering, set to FLT_MAX to never show close button unless selected.
    float       TabBarBorderSize;           // Thickness of tab-bar separator, which takes on the tab active color to denote focus.
    float       TableAngledHeadersAngle;    // Angle of angled headers (supported values range from -50.0f degrees to +50.0f degrees).
    ImGuiDir    ColorButtonPosition;        // Side of the color button in the ColorEdit4 widget (left/right). Defaults to Right.
    float2      ButtonTextAlign;            // Alignment of button text when button is larger than text. Defaults to (0.5f, 0.5f) (centered).
    float2      SelectableTextAlign;        // Alignment of selectable text. Defaults to (0.0f, 0.0f) (top-left aligned). It's generally important to keep this left-aligned if you want to lay multiple items on a same line.
    float       SeparatorTextBorderSize;    // Thickkness of border in SeparatorText()
    float2      SeparatorTextAlign;         // Alignment of text within the separator. Defaults to (0.0f, 0.5f) (left aligned, center).
    float2      SeparatorTextPadding;       // Horizontal offset of text from each edge of the separator + spacing on other axis. Generally small values. .y is recommended to be == FramePadding.y.
    float2      DisplayWindowPadding;       // Window position are clamped to be visible within the display area or monitors by at least this amount. Only applies to regular windows.
    float2      DisplaySafeAreaPadding;     // If you cannot see the edges of your screen (e.g. on a TV) increase the safe area padding. Apply to popups/tooltips as well regular windows. NB: Prefer configuring your TV sets correctly!
    float       DockingSeparatorSize;       // Thickness of resizing border between docked windows
    float       MouseCursorScale;           // Scale software rendered mouse cursor (when io.MouseDrawCursor is enabled). We apply per-monitor DPI scaling over this scale. May be removed later.
    bool        AntiAliasedLines;           // Enable anti-aliased lines/borders. Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).
    bool        AntiAliasedLinesUseTex;     // Enable anti-aliased lines/borders using textures where possible. Require backend to render with bilinear filtering (NOT point/nearest filtering). Latched at the beginning of the frame (copied to ImDrawList).
    bool        AntiAliasedFill;            // Enable anti-aliased edges around filled shapes (rounded rectangles, circles, etc.). Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).
    float       CurveTessellationTol;       // Tessellation tolerance when using PathBezierCurveTo() without a specific number of segments. Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.
    float       CircleTessellationMaxError; // Maximum error (in pixels) allowed when using AddCircle()/AddCircleFilled() or drawing rounded corner rectangles with no explicit segment count specified. Decrease for higher quality but more geometry.
    internal ColorArray  Colors;

    // Behaviors
    // (It is possible to modify those fields mid-frame if specific behavior need it, unlike e.g. configuration fields in ImGuiIO)
    float             HoverStationaryDelay;     // Delay for IsItemHovered(Stationary). Time required to consider mouse stationary.
    float             HoverDelayShort;          // Delay for IsItemHovered(DelayShort). Usually used along with HoverStationaryDelay.
    float             HoverDelayNormal;         // Delay for IsItemHovered(DelayNormal). "
    ImGuiHoveredFlags HoverFlagsForTooltipMouse;// Default flags when using IsItemHovered(ForTooltip) or BeginItemTooltip()/SetItemTooltip() while using mouse.
    ImGuiHoveredFlags HoverFlagsForTooltipNav;  // Default flags when using IsItemHovered(ForTooltip) or BeginItemTooltip()/SetItemTooltip() while using keyboard/gamepad.

    // IMGUI_API ImGuiStyle();
    // IMGUI_API void ScaleAllSizes(float scale_factor);
}

enum ImGuiHoveredFlags
{
    None                          = 0,        // Return true if directly over the item/window, not obstructed by another window, not obstructed by an active popup or modal blocking inputs under them.
    ChildWindows                  = 1 << 0,   // IsWindowHovered() only: Return true if any children of the window is hovered
    RootWindow                    = 1 << 1,   // IsWindowHovered() only: Test from root window (top most parent of the current hierarchy)
    AnyWindow                     = 1 << 2,   // IsWindowHovered() only: Return true if any window is hovered
    NoPopupHierarchy              = 1 << 3,   // IsWindowHovered() only: Do not consider popup hierarchy (do not treat popup emitter as parent of popup) (when used with _ChildWindows or _RootWindow)
    DockHierarchy                 = 1 << 4,   // IsWindowHovered() only: Consider docking hierarchy (treat dockspace host as parent of docked window) (when used with _ChildWindows or _RootWindow)
    AllowWhenBlockedByPopup       = 1 << 5,   // Return true even if a popup window is normally blocking access to this item/window
    //AllowWhenBlockedByModal     = 1 << 6,   // Return true even if a modal popup window is normally blocking access to this item/window. FIXME-TODO: Unavailable yet.
    AllowWhenBlockedByActiveItem  = 1 << 7,   // Return true even if an active item is blocking access to this item/window. Useful for Drag and Drop patterns.
    AllowWhenOverlappedByItem     = 1 << 8,   // IsItemHovered() only: Return true even if the item uses AllowOverlap mode and is overlapped by another hoverable item.
    AllowWhenOverlappedByWindow   = 1 << 9,   // IsItemHovered() only: Return true even if the position is obstructed or overlapped by another window.
    AllowWhenDisabled             = 1 << 10,  // IsItemHovered() only: Return true even if the item is disabled
    NoNavOverride                 = 1 << 11,  // IsItemHovered() only: Disable using gamepad/keyboard navigation state when active, always query mouse
    AllowWhenOverlapped           = AllowWhenOverlappedByItem | AllowWhenOverlappedByWindow,
    RectOnly                      = AllowWhenBlockedByPopup | AllowWhenBlockedByActiveItem | AllowWhenOverlapped,
    RootAndChildWindows           = RootWindow | ChildWindows,

    // Tooltips mode
    // - typically used in IsItemHovered() + SetTooltip() sequence.
    // - this is a shortcut to pull flags from 'style.HoverFlagsForTooltipMouse' or 'style.HoverFlagsForTooltipNav' where you can reconfigure desired behavior.
    //   e.g. 'TooltipHoveredFlagsForMouse' defaults to 'Stationary | DelayShort'.
    // - for frequently actioned or hovered items providing a tooltip, you want may to use ForTooltip (stationary + delay) so the tooltip doesn't show too often.
    // - for items which main purpose is to be hovered, or items with low affordance, or in less consistent apps, prefer no delay or shorter delay.
    ForTooltip                    = 1 << 12,  // Shortcut for standard flags when using IsItemHovered() + SetTooltip() sequence.

    // (Advanced) Mouse Hovering delays.
    // - generally you can use ForTooltip to use application-standardized flags.
    // - use those if you need specific overrides.
    Stationary                    = 1 << 13,  // Require mouse to be stationary for style.HoverStationaryDelay (~0.15 sec) _at least one time_. After this, can move on same item/window. Using the stationary test tends to reduces the need for a long delay.
    DelayNone                     = 1 << 14,  // IsItemHovered() only: Return true immediately (default). As this is the default you generally ignore this.
    DelayShort                    = 1 << 15,  // IsItemHovered() only: Return true after style.HoverDelayShort elapsed (~0.15 sec) (shared between items) + requires mouse to be stationary for style.HoverStationaryDelay (once per item).
    DelayNormal                   = 1 << 16,  // IsItemHovered() only: Return true after style.HoverDelayNormal elapsed (~0.40 sec) (shared between items) + requires mouse to be stationary for style.HoverStationaryDelay (once per item).
    NoSharedDelay                 = 1 << 17,  // IsItemHovered() only: Disable shared delay system where moving from one item to the next keeps the previous timer for a short time (standard for tooltips with long delays)
};

[StructLayout(LayoutKind.Explicit)]
unsafe struct ColorArray
{
	[FieldOffset(0)]
	fixed float Values[(int)ImGuiCol.COUNT*4];

	internal ref float4 this[ImGuiCol index] => ref UnsafeUtility.ArrayElementAsRef<float4>(UnsafeUtility.AddressOf(ref this), (int)index);
}

enum ImGuiCol
{
    Text,
    TextDisabled,
    WindowBg,              // Background of normal windows
    ChildBg,               // Background of child windows
    PopupBg,               // Background of popups, menus, tooltips windows
    Border,
    BorderShadow,
    FrameBg,               // Background of checkbox, radio button, plot, slider, text input
    FrameBgHovered,
    FrameBgActive,
    TitleBg,               // Title bar
    TitleBgActive,         // Title bar when focused
    TitleBgCollapsed,      // Title bar when collapsed
    MenuBarBg,
    ScrollbarBg,
    ScrollbarGrab,
    ScrollbarGrabHovered,
    ScrollbarGrabActive,
    CheckMark,             // Checkbox tick and RadioButton circle
    SliderGrab,
    SliderGrabActive,
    Button,
    ButtonHovered,
    ButtonActive,
    Header,                // Header* colors are used for CollapsingHeader, TreeNode, Selectable, MenuItem
    HeaderHovered,
    HeaderActive,
    Separator,
    SeparatorHovered,
    SeparatorActive,
    ResizeGrip,            // Resize grip in lower-right and lower-left corners of windows.
    ResizeGripHovered,
    ResizeGripActive,
    Tab,                   // TabItem in a TabBar
    TabHovered,
    TabActive,
    TabUnfocused,
    TabUnfocusedActive,
    DockingPreview,        // Preview overlay color when about to docking something
    DockingEmptyBg,        // Background color for empty node (e.g. CentralNode with no window docked into it)
    PlotLines,
    PlotLinesHovered,
    PlotHistogram,
    PlotHistogramHovered,
    TableHeaderBg,         // Table header background
    TableBorderStrong,     // Table outer and header borders (prefer using Alpha=1.0 here)
    TableBorderLight,      // Table inner borders (prefer using Alpha=1.0 here)
    TableRowBg,            // Table row background (even rows)
    TableRowBgAlt,         // Table row background (odd rows)
    TextSelectedBg,
    DragDropTarget,        // Rectangle highlighting a drop target
    NavHighlight,          // Gamepad/keyboard: current highlighted item
    NavWindowingHighlight, // Highlight window when using CTRL+TAB
    NavWindowingDimBg,     // Darken/colorize entire screen behind the CTRL+TAB window list, when active
    ModalWindowDimBg,      // Darken/colorize entire screen behind a modal window, when one is active
    COUNT
};


[StructLayout(LayoutKind.Sequential)]
struct ImDrawData
{
	public byte                Valid;              // Only valid after Render() is called and before the next NewFrame() is called.
    public int                 CmdListsCount;      // Number of ImDrawList* to render (should always be == CmdLists.size)
    public int                 TotalIdxCount;      // For convenience, sum of all ImDrawList's IdxBuffer.Size
    public int                 TotalVtxCount;      // For convenience, sum of all ImDrawList's VtxBuffer.Size
    public ImVector<Ptr<ImDrawList>> CmdLists;         // Array of ImDrawList* to render. The ImDrawLists are owned by ImGuiContext and only pointed to from here.
    public float2              DisplayPos;         // Top-left position of the viewport to render (== top-left of the orthogonal projection matrix to use) (== GetMainViewport()->Pos for the main viewport, == (0.0) in most single-viewport applications)
    public float2              DisplaySize;        // Size of the viewport to render (== GetMainViewport()->Size for the main viewport, == io.DisplaySize in most single-viewport applications)
    public float2              FramebufferScale;   // Amount of pixels for each unit of DisplaySize. Based on io.DisplayFramebufferScale. Generally (1,1) on normal display, (2,2) on OSX with Retina display.
    public IntPtr      OwnerViewport;      // Viewport carrying the ImDrawData instance, might be of use to the renderer (generally not). // ImGuiViewport

    // // Functions
    // ImDrawData()    { Clear(); }
    // void  Clear() {}
    // void  AddDrawList(ref ImDrawList draw_list) {}
    // void  DeIndexAllBuffers() {}
    // void  ScaleClipRects(ref float2 fb_scale) {}
}

[StructLayout(LayoutKind.Sequential)]
unsafe struct Ptr<T> where T : unmanaged
{
	public T* Value;
}

[StructLayout(LayoutKind.Sequential)]
unsafe struct ImVector<T> where T : unmanaged
{
    public int                 Size;
    public int                 Capacity;
    public T*                  Data;
    
    public ref T this[int index] => ref UnsafeUtility.ArrayElementAsRef<T>(Data, index);
}

[StructLayout(LayoutKind.Sequential)]
unsafe struct ImVectorRaw
{
	int                 Size;
	int                 Capacity;
	public void*               Data;
}

unsafe struct ImDrawCallback
{
	public delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void> Value;
	public static delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void> ResetRenderState => (delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void>)k_resetRenderState;
	const nint k_resetRenderState = -8;
}

[StructLayout(LayoutKind.Sequential)]
unsafe struct ImDrawCmd
{
	public float4          ClipRect;           // 4*4  // Clipping rectangle (x1, y1, x2, y2). Subtract ImDrawData->DisplayPos to get clipping rectangle in "viewport" coordinates
	ImTextureID     TextureId;          // 4-8  // User-provided texture ID. Set by user in ImfontAtlas::SetTexID() for fonts or passed to Image*() functions. Ignore if never using images or multiple fonts atlas.
	public uint    VtxOffset;          // 4    // Start offset in vertex buffer. ImGuiBackendFlags_RendererHasVtxOffset: always 0, otherwise may be >0 to support meshes larger than 64K vertices with 16-bit indices.
	public uint    IdxOffset;          // 4    // Start offset in index buffer.
	public uint    ElemCount;          // 4    // Number of indices (multiple of 3) to be rendered as triangles. Vertices are stored in the callee ImDrawList's vtx_buffer[] array, indices in idx_buffer[].
	public ImDrawCallback  UserCallback;       // 4-8  // If != NULL, call the function instead of rendering the vertices. clip_rect and texture_id will be set normally.
	void*           UserCallbackData;   // 4-8  // The draw callback code can access this.
	
	// Since 1.83: returns ImTextureID associated with this draw call. Warning: DO NOT assume this is always same as 'TextureId' (we will change this function for an upcoming feature)
	public ImTextureID GetTexID() => TextureId; 
};

[StructLayout(LayoutKind.Sequential)]
unsafe struct ImDrawList
{
    // This is what you have to render
    public ImVector<ImDrawCmd>   CmdBuffer;          // Draw commands. Typically 1 command = 1 GPU draw call, unless the command is a callback. // ImDrawCmd
    public ImVector<ImDrawIdx>    IdxBuffer;          // Index buffer. Each command consume ImDrawCmd::ElemCount of those
    public ImVector<ImDrawVert>    VtxBuffer;          // Vertex buffer.
    public ImDrawListFlags         Flags;              // Flags, you may poke into these to adjust anti-aliasing settings per-primitive.

    // [Internal, used while building lists]
    public uint            _VtxCurrentIdx;     // [Internal] generally == VtxBuffer.Size unless we are past 64K vertices, in which case this gets reset to 0.
    public IntPtr   _Data;              // Pointer to shared draw data (you can use ImGui::GetDrawListSharedData() to get the one from current ImGui context) // ImDrawListSharedData
    public char*             _OwnerName;         // Pointer to owner window's name for debugging
    public IntPtr             _VtxWritePtr;       // [Internal] point within VtxBuffer.Data after each add command (to avoid using the ImVector<> operators too much) // ImDrawVert
    public IntPtr              _IdxWritePtr;       // [Internal] point within IdxBuffer.Data after each add command (to avoid using the ImVector<> operators too much) // ImDrawIdx
    public ImVector<float4>        _ClipRectStack;     // [Internal]
    public ImVector<ImTextureID>   _TextureIdStack;    // [Internal]
    public ImVector<float2>        _Path;              // [Internal] current path building
    public ImDrawCmdHeader         _CmdHeader;         // [Internal] template of active commands. Fields should match those of CmdBuffer.back().
    public ImDrawListSplitter      _Splitter;          // [Internal] for channels api (note: prefer using your own persistent instance of ImDrawListSplitter!)
    public float                   _FringeScale;       // [Internal] anti-alias fringe is scaled by this value, this helps to keep things sharp while zooming at vertex buffer content

    // // If you want to create ImDrawList instances, pass them ImGui::GetDrawListSharedData() or create and use your own ImDrawListSharedData (so you can use ImDrawList without ImGui)
    // ImDrawList(ImDrawListSharedData* shared_data) { memset(this, 0, sizeof(*this)); _Data = shared_data; }
    //
    // ~ImDrawList() { _ClearFreeMemory(); }
    // IMGUI_API void  PushClipRect(const float2& clip_rect_min, const float2& clip_rect_max, bool intersect_with_current_clip_rect = false);  // Render-level scissoring. This is passed down to your render function but not used for CPU-side coarse clipping. Prefer using higher-level ImGui::PushClipRect() to affect logic (hit-testing and widget culling)
    // IMGUI_API void  PushClipRectFullScreen();
    // IMGUI_API void  PopClipRect();
    // IMGUI_API void  PushTextureID(ImTextureID texture_id);
    // IMGUI_API void  PopTextureID();
    // inline float2   GetClipRectMin() const { const ImVec4& cr = _ClipRectStack.back(); return float2(cr.x, cr.y); }
    // inline float2   GetClipRectMax() const { const ImVec4& cr = _ClipRectStack.back(); return float2(cr.z, cr.w); }
    //
    // // Primitives
    // // - Filled shapes must always use clockwise winding order. The anti-aliasing fringe depends on it. Counter-clockwise shapes will have "inward" anti-aliasing.
    // // - For rectangular primitives, "p_min" and "p_max" represent the upper-left and lower-right corners.
    // // - For circle primitives, use "num_segments == 0" to automatically calculate tessellation (preferred).
    // //   In older versions (until Dear ImGui 1.77) the AddCircle functions defaulted to num_segments == 12.
    // //   In future versions we will use textures to provide cheaper and higher-quality circles.
    // //   Use AddNgon() and AddNgonFilled() functions if you need to guarantee a specific number of sides.
    // IMGUI_API void  AddLine(const float2& p1, const float2& p2, ImU32 col, float thickness = 1.0f);
    // IMGUI_API void  AddRect(const float2& p_min, const float2& p_max, ImU32 col, float rounding = 0.0f, ImDrawFlags flags = 0, float thickness = 1.0f);   // a: upper-left, b: lower-right (== upper-left + size)
    // IMGUI_API void  AddRectFilled(const float2& p_min, const float2& p_max, ImU32 col, float rounding = 0.0f, ImDrawFlags flags = 0);                     // a: upper-left, b: lower-right (== upper-left + size)
    // IMGUI_API void  AddRectFilledMultiColor(const float2& p_min, const float2& p_max, ImU32 col_upr_left, ImU32 col_upr_right, ImU32 col_bot_right, ImU32 col_bot_left);
    // IMGUI_API void  AddQuad(const float2& p1, const float2& p2, const float2& p3, const float2& p4, ImU32 col, float thickness = 1.0f);
    // IMGUI_API void  AddQuadFilled(const float2& p1, const float2& p2, const float2& p3, const float2& p4, ImU32 col);
    // IMGUI_API void  AddTriangle(const float2& p1, const float2& p2, const float2& p3, ImU32 col, float thickness = 1.0f);
    // IMGUI_API void  AddTriangleFilled(const float2& p1, const float2& p2, const float2& p3, ImU32 col);
    // IMGUI_API void  AddCircle(const float2& center, float radius, ImU32 col, int num_segments = 0, float thickness = 1.0f);
    // IMGUI_API void  AddCircleFilled(const float2& center, float radius, ImU32 col, int num_segments = 0);
    // IMGUI_API void  AddNgon(const float2& center, float radius, ImU32 col, int num_segments, float thickness = 1.0f);
    // IMGUI_API void  AddNgonFilled(const float2& center, float radius, ImU32 col, int num_segments);
    // IMGUI_API void  AddEllipse(const float2& center, float radius_x, float radius_y, ImU32 col, float rot = 0.0f, int num_segments = 0, float thickness = 1.0f);
    // IMGUI_API void  AddEllipseFilled(const float2& center, float radius_x, float radius_y, ImU32 col, float rot = 0.0f, int num_segments = 0);
    // IMGUI_API void  AddText(const float2& pos, ImU32 col, const char* text_begin, const char* text_end = NULL);
    // IMGUI_API void  AddText(const ImFont* font, float font_size, const float2& pos, ImU32 col, const char* text_begin, const char* text_end = NULL, float wrap_width = 0.0f, const ImVec4* cpu_fine_clip_rect = NULL);
    // IMGUI_API void  AddPolyline(const float2* points, int num_points, ImU32 col, ImDrawFlags flags, float thickness);
    // IMGUI_API void  AddConvexPolyFilled(const float2* points, int num_points, ImU32 col);
    // IMGUI_API void  AddBezierCubic(const float2& p1, const float2& p2, const float2& p3, const float2& p4, ImU32 col, float thickness, int num_segments = 0); // Cubic Bezier (4 control points)
    // IMGUI_API void  AddBezierQuadratic(const float2& p1, const float2& p2, const float2& p3, ImU32 col, float thickness, int num_segments = 0);               // Quadratic Bezier (3 control points)
    //
    // // Image primitives
    // // - Read FAQ to understand what ImTextureID is.
    // // - "p_min" and "p_max" represent the upper-left and lower-right corners of the rectangle.
    // // - "uv_min" and "uv_max" represent the normalized texture coordinates to use for those corners. Using (0,0)->(1,1) texture coordinates will generally display the entire texture.
    // IMGUI_API void  AddImage(ImTextureID user_texture_id, const float2& p_min, const float2& p_max, const float2& uv_min = float2(0, 0), const float2& uv_max = float2(1, 1), ImU32 col = IM_COL32_WHITE);
    // IMGUI_API void  AddImageQuad(ImTextureID user_texture_id, const float2& p1, const float2& p2, const float2& p3, const float2& p4, const float2& uv1 = float2(0, 0), const float2& uv2 = float2(1, 0), const float2& uv3 = float2(1, 1), const float2& uv4 = float2(0, 1), ImU32 col = IM_COL32_WHITE);
    // IMGUI_API void  AddImageRounded(ImTextureID user_texture_id, const float2& p_min, const float2& p_max, const float2& uv_min, const float2& uv_max, ImU32 col, float rounding, ImDrawFlags flags = 0);
    //
    // // Stateful path API, add points then finish with PathFillConvex() or PathStroke()
    // // - Filled shapes must always use clockwise winding order. The anti-aliasing fringe depends on it. Counter-clockwise shapes will have "inward" anti-aliasing.
    // inline    void  PathClear()                                                 { _Path.Size = 0; }
    // inline    void  PathLineTo(const float2& pos)                               { _Path.push_back(pos); }
    // inline    void  PathLineToMergeDuplicate(const float2& pos)                 { if (_Path.Size == 0 || memcmp(&_Path.Data[_Path.Size - 1], &pos, 8) != 0) _Path.push_back(pos); }
    // inline    void  PathFillConvex(ImU32 col)                                   { AddConvexPolyFilled(_Path.Data, _Path.Size, col); _Path.Size = 0; }
    // inline    void  PathStroke(ImU32 col, ImDrawFlags flags = 0, float thickness = 1.0f) { AddPolyline(_Path.Data, _Path.Size, col, flags, thickness); _Path.Size = 0; }
    // IMGUI_API void  PathArcTo(const float2& center, float radius, float a_min, float a_max, int num_segments = 0);
    // IMGUI_API void  PathArcToFast(const float2& center, float radius, int a_min_of_12, int a_max_of_12);                // Use precomputed angles for a 12 steps circle
    // IMGUI_API void  PathEllipticalArcTo(const float2& center, float radius_x, float radius_y, float rot, float a_min, float a_max, int num_segments = 0); // Ellipse
    // IMGUI_API void  PathBezierCubicCurveTo(const float2& p2, const float2& p3, const float2& p4, int num_segments = 0); // Cubic Bezier (4 control points)
    // IMGUI_API void  PathBezierQuadraticCurveTo(const float2& p2, const float2& p3, int num_segments = 0);               // Quadratic Bezier (3 control points)
    // IMGUI_API void  PathRect(const float2& rect_min, const float2& rect_max, float rounding = 0.0f, ImDrawFlags flags = 0);
    //
    // // Advanced
    // IMGUI_API void  AddCallback(ImDrawCallback callback, void* callback_data);  // Your rendering function must check for 'UserCallback' in ImDrawCmd and call the function instead of rendering triangles.
    // IMGUI_API void  AddDrawCmd();                                               // This is useful if you need to forcefully create a new draw call (to allow for dependent rendering / blending). Otherwise primitives are merged into the same draw-call as much as possible
    // IMGUI_API ImDrawList* CloneOutput() const;                                  // Create a clone of the CmdBuffer/IdxBuffer/VtxBuffer.
    //
    // // Advanced: Channels
    // // - Use to split render into layers. By switching channels to can render out-of-order (e.g. submit FG primitives before BG primitives)
    // // - Use to minimize draw calls (e.g. if going back-and-forth between multiple clipping rectangles, prefer to append into separate channels then merge at the end)
    // // - This API shouldn't have been in ImDrawList in the first place!
    // //   Prefer using your own persistent instance of ImDrawListSplitter as you can stack them.
    // //   Using the ImDrawList::ChannelsXXXX you cannot stack a split over another.
    // inline void     ChannelsSplit(int count)    { _Splitter.Split(this, count); }
    // inline void     ChannelsMerge()             { _Splitter.Merge(this); }
    // inline void     ChannelsSetCurrent(int n)   { _Splitter.SetCurrentChannel(this, n); }
    //
    // // Advanced: Primitives allocations
    // // - We render triangles (three vertices)
    // // - All primitives needs to be reserved via PrimReserve() beforehand.
    // IMGUI_API void  PrimReserve(int idx_count, int vtx_count);
    // IMGUI_API void  PrimUnreserve(int idx_count, int vtx_count);
    // IMGUI_API void  PrimRect(const float2& a, const float2& b, ImU32 col);      // Axis aligned rectangle (composed of two triangles)
    // IMGUI_API void  PrimRectUV(const float2& a, const float2& b, const float2& uv_a, const float2& uv_b, ImU32 col);
    // IMGUI_API void  PrimQuadUV(const float2& a, const float2& b, const float2& c, const float2& d, const float2& uv_a, const float2& uv_b, const float2& uv_c, const float2& uv_d, ImU32 col);
    // inline    void  PrimWriteVtx(const float2& pos, const float2& uv, ImU32 col)    { _VtxWritePtr->pos = pos; _VtxWritePtr->uv = uv; _VtxWritePtr->col = col; _VtxWritePtr++; _VtxCurrentIdx++; }
    // inline    void  PrimWriteIdx(ImDrawIdx idx)                                     { *_IdxWritePtr = idx; _IdxWritePtr++; }
    // inline    void  PrimVtx(const float2& pos, const float2& uv, ImU32 col)         { PrimWriteIdx((ImDrawIdx)_VtxCurrentIdx); PrimWriteVtx(pos, uv, col); } // Write vertex with unique index
    //
    // // Obsolete names
    // //inline  void  AddBezierCurve(const float2& p1, const float2& p2, const float2& p3, const float2& p4, ImU32 col, float thickness, int num_segments = 0) { AddBezierCubic(p1, p2, p3, p4, col, thickness, num_segments); } // OBSOLETED in 1.80 (Jan 2021)
    // //inline  void  PathBezierCurveTo(const float2& p2, const float2& p3, const float2& p4, int num_segments = 0) { PathBezierCubicCurveTo(p2, p3, p4, num_segments); } // OBSOLETED in 1.80 (Jan 2021)
    //
    // // [Internal helpers]
    // IMGUI_API void  _ResetForNewFrame();
    // IMGUI_API void  _ClearFreeMemory();
    // IMGUI_API void  _PopUnusedDrawCmd();
    // IMGUI_API void  _TryMergeDrawCmds();
    // IMGUI_API void  _OnChangedClipRect();
    // IMGUI_API void  _OnChangedTextureID();
    // IMGUI_API void  _OnChangedVtxOffset();
    // IMGUI_API int   _CalcCircleAutoSegmentCount(float radius) const;
    // IMGUI_API void  _PathArcToFastEx(const float2& center, float radius, int a_min_sample, int a_max_sample, int a_step);
    // IMGUI_API void  _PathArcToN(const float2& center, float radius, float a_min, float a_max, int num_segments);
}

struct ImDrawChannel
{
	ImVectorRaw         _CmdBuffer; // ImDrawCmd
	ImVectorRaw         _IdxBuffer; // ImDrawIdx
};

[Flags]
enum ImDrawListFlags
{
	None                    = 0,
	AntiAliasedLines        = 1 << 0,  // Enable anti-aliased lines/borders (*2 the number of triangles for 1.0f wide line or lines thin enough to be drawn using textures, otherwise *3 the number of triangles)
	AntiAliasedLinesUseTex  = 1 << 1,  // Enable anti-aliased lines/borders using textures when possible. Require backend to render with bilinear filtering (NOT point/nearest filtering).
	AntiAliasedFill         = 1 << 2,  // Enable anti-aliased edge around filled shapes (rounded rectangles, circles).
	AllowVtxOffset          = 1 << 3,  // Can emit 'VtxOffset > 0' to allow large meshes. Set when 'RendererHasVtxOffset' is enabled.
};

[StructLayout(LayoutKind.Sequential)]
struct ImDrawListSplitter
{
	int                         _Current;    // Current channel number (0)
	int                         _Count;      // Number of active channels (1+)
	ImVector<ImDrawChannel>     _Channels;   // Draw channels (not resized down so _Count might be < Channels.Size)

	// inline ImDrawListSplitter()  { memset(this, 0, sizeof(*this)); }
	// inline ~ImDrawListSplitter() { ClearFreeMemory(); }
	// inline void                 Clear() { _Current = 0; _Count = 1; } // Do not clear Channels[] so our allocations are reused next frame
	// IMGUI_API void              ClearFreeMemory();
	// IMGUI_API void              Split(ImDrawList* draw_list, int count);
	// IMGUI_API void              Merge(ImDrawList* draw_list);
	// IMGUI_API void              SetCurrentChannel(ImDrawList* draw_list, int channel_idx);
};

[StructLayout(LayoutKind.Sequential)]
struct ImDrawCmdHeader
{
	float4          ClipRect;
	ImTextureID     TextureId;
	uint    VtxOffset;
};

unsafe struct ImTextureID
{
	public void* Value;
}

struct ImWchar
{
#if IMGUI_USE_WCHAR32
	public uint Value;
#else
	public ushort Value;
#endif
}

[Flags]
enum ImGuiConfigFlags
{
	None                   = 0,
	NavEnableKeyboard      = 1 << 0,   // Master keyboard navigation enable flag. Enable full Tabbing + directional arrows + space/enter to activate.
	NavEnableGamepad       = 1 << 1,   // Master gamepad navigation enable flag. Backend also needs to set HasGamepad.
	NavEnableSetMousePos   = 1 << 2,   // Instruct navigation to move the mouse cursor. May be useful on TV/console systems where moving a virtual mouse is awkward. Will update io.MousePos and set io.WantSetMousePos=true. If enabled you MUST honor io.WantSetMousePos requests in your backend, otherwise ImGui will react as if the mouse is jumping around back and forth.
	NavNoCaptureKeyboard   = 1 << 3,   // Instruct navigation to not set the io.WantCaptureKeyboard flag when io.NavActive is set.
	NoMouse                = 1 << 4,   // Instruct imgui to clear mouse position/buttons in NewFrame(). This allows ignoring the mouse information set by the backend.
	NoMouseCursorChange    = 1 << 5,   // Instruct backend to not alter mouse cursor shape and visibility. Use if the backend cursor changes are interfering with yours and you don't want to use SetMouseCursor() to change mouse cursor. You may want to honor requests from imgui by reading GetMouseCursor() yourself instead.

	DockingEnable = 1 << 6,
	
	// User storage (to allow your backend/engine to communicate to code that may be shared between multiple projects. Those flags are NOT used by core Dear ImGui)
	IsSRGB                 = 1 << 20,  // Application is SRGB-aware.
	IsTouchScreen          = 1 << 21,  // Application is using a touch screen instead of a mouse.
};

[Flags]
enum ImGuiBackendFlags
{
	None                  = 0,
	HasGamepad            = 1 << 0,   // Backend Platform supports gamepad and currently has one connected.
	HasMouseCursors       = 1 << 1,   // Backend Platform supports honoring GetMouseCursor() value to change the OS cursor shape.
	HasSetMousePos        = 1 << 2,   // Backend Platform supports io.WantSetMousePos requests to reposition the OS mouse position (only used if ImGuiConfigFlags_NavEnableSetMousePos is set).
	RendererHasVtxOffset  = 1 << 3,   // Backend Renderer supports ImDrawCmd::VtxOffset. This enables output of large meshes (64K+ vertices) while still using 16-bit indices.
};

// Only defined for use as a pointer type. This is a low-level data type, not used directly by end-users.
struct ImGuiContext {}

struct ImGuiID
{
	public uint Value;
}

[StructLayout(LayoutKind.Sequential)]
unsafe struct ImGuiIO
{
	//------------------------------------------------------------------
    // Configuration                            // Default value
    //------------------------------------------------------------------

    public ImGuiConfigFlags   ConfigFlags;             // = 0              // See ImGuiConfigFlags_ enum. Set by user/application. Gamepad/keyboard navigation options, etc.
    public ImGuiBackendFlags  BackendFlags;            // = 0              // See ImGuiBackendFlags_ enum. Set by backend (imgui_impl_xxx files or custom backend) to communicate features supported by the backend.
    public float2      DisplaySize;                    // <unset>          // Main display size, in pixels (generally == GetMainViewport()->Size). May change every frame.
    public float       DeltaTime;                      // = 1.0f/60.0f     // Time elapsed since last frame, in seconds. May change every frame.
    float       IniSavingRate;                  // = 5.0f           // Minimum time between saving positions/sizes to .ini file, in seconds.
    char* IniFilename;                    // = "imgui.ini"    // Path to .ini file (important: default "imgui.ini" is relative to current working dir!). Set NULL to disable automatic .ini loading/saving or if you want to manually call LoadIniSettingsXXX() / SaveIniSettingsXXX() functions.
    char* LogFilename;                    // = "imgui_log.txt"// Path to .log file (default parameter to ImGui::LogToFile when no file is specified).
    void*       UserData;                       // = NULL           // Store your own data.

    public ImFontAtlas* Fonts;                          // <auto>           // Font atlas: load, rasterize and pack one or more fonts into a single texture. // ImFontAtlas
    public float       FontGlobalScale;                // = 1.0f           // Global scale all fonts
    byte        FontAllowUserScaling;           // = false          // Allow user scaling text of individual window with CTRL+Wheel.
    public ImFontAtlas*     FontDefault;                    // = NULL           // Font to use on NewFrame(). Use NULL to uses Fonts->Fonts[0]. // ImFont
    public float2      DisplayFramebufferScale;        // = (1, 1)         // For retina display or other situations where window coordinates are different from framebuffer coordinates. This generally ends up in ImDrawData::FramebufferScale.

    // Docking options (when ImGuiConfigFlags_DockingEnable is set)
    byte        ConfigDockingNoSplit;           // = false          // Simplified docking mode: disable window splitting, so docking is limited to merging multiple windows together into tab-bars.
    byte        ConfigDockingWithShift;         // = false          // Enable docking with holding Shift key (reduce visual noise, allows dropping in wider space)
    byte        ConfigDockingAlwaysTabBar;      // = false          // [BETA] [FIXME: This currently creates regression with auto-sizing and general overhead] Make every single floating window display within a docking node.
    byte        ConfigDockingTransparentPayload;// = false          // [BETA] Make window or viewport transparent when docking and only display docking boxes on the target viewport. Useful if rendering of multiple viewport cannot be synced. Best used with ConfigViewportsNoAutoMerge.

    // Viewport options (when ImGuiConfigFlags_ViewportsEnable is set)
    byte        ConfigViewportsNoAutoMerge;     // = false;         // Set to make all floating imgui windows always create their own viewport. Otherwise, they are merged into the main host viewports when overlapping it. May also set NoAutoMerge on individual viewport.
    byte        ConfigViewportsNoTaskBarIcon;   // = false          // Disable default OS task bar icon flag for secondary viewports. When a viewport doesn't want a task bar icon, NoTaskBarIcon will be set on it.
    byte        ConfigViewportsNoDecoration;    // = true           // Disable default OS window decoration flag for secondary viewports. When a viewport doesn't want window decorations, NoDecoration will be set on it. Enabling decoration can create subsequent issues at OS levels (e.g. minimum window size).
    byte        ConfigViewportsNoDefaultParent; // = false          // Disable default OS parenting to main viewport for secondary viewports. By default, viewports are marked with ParentViewportId = <main_viewport>, expecting the platform backend to setup a parent/child relationship between the OS windows (some backend may ignore this). Set to true if you want the default to be 0, then all viewports will be top-level OS windows.

    // Miscellaneous options
    byte        MouseDrawCursor;                // = false          // Request ImGui to draw a mouse cursor for you (if you are on a platform without a mouse cursor). Cannot be easily renamed to 'io.ConfigXXX' because this is frequently used by backend implementations.
    byte        ConfigMacOSXBehaviors;          // = defined(__APPLE__) // OS X style: Text editing cursor movement using Alt instead of Ctrl, Shortcuts using Cmd/Super instead of Ctrl, Line/Text Start and End using Cmd+Arrows instead of Home/End, Double click selects by word instead of selecting whole text, Multi-selection in lists uses Cmd/Super instead of Ctrl.
    byte        ConfigInputTrickleEventQueue;   // = true           // Enable input queue trickling: some types of events submitted during the same frame (e.g. button down + up) will be spread over multiple frames, improving interactions with low framerates.
    byte        ConfigInputTextCursorBlink;     // = true           // Enable blinking cursor (optional as some users consider it to be distracting).
    byte        ConfigInputTextEnterKeepActive; // = false          // [BETA] Pressing Enter will keep item active and select contents (single-line only).
    byte        ConfigDragClickToInputText;     // = false          // [BETA] Enable turning DragXXX widgets into text input with a simple mouse click-release (without moving). Not desirable on devices without a keyboard.
    byte        ConfigWindowsResizeFromEdges;   // = true           // Enable resizing of windows from their edges and from the lower-left corner. This requires (io.BackendFlags & ImGuiBackendFlags_HasMouseCursors) because it needs mouse cursor feedback. (This used to be a per-window ImGuiWindowFlags_ResizeFromAnySide flag)
    byte        ConfigWindowsMoveFromTitleBarOnly; // = false       // Enable allowing to move windows only when clicking on their title bar. Does not apply to windows without a title bar.
    float       ConfigMemoryCompactTimer;       // = 60.0f          // Timer (in seconds) to free transient windows/tables memory buffers when unused. Set to -1.0f to disable.

    // Inputs Behaviors
    // (other variables, ones which are expected to be tweaked within UI code, are exposed in ImGuiStyle)
    float       MouseDoubleClickTime;           // = 0.30f          // Time for a double-click, in seconds.
    float       MouseDoubleClickMaxDist;        // = 6.0f           // Distance threshold to stay in to validate a double-click, in pixels.
    float       MouseDragThreshold;             // = 6.0f           // Distance threshold before considering we are dragging.
    float       KeyRepeatDelay;                 // = 0.275f         // When holding a key/button, time before it starts repeating, in seconds (for buttons in Repeat mode, etc.).
    float       KeyRepeatRate;                  // = 0.050f         // When holding a key/button, rate at which it repeats, in seconds.

    //------------------------------------------------------------------
    // Debug options
    //------------------------------------------------------------------

    // Option to enable various debug tools showing buttons that will call the IM_DEBUG_BREAK() macro.
    // - The Item Picker tool will be available regardless of this being enabled, in order to maximize its discoverability.
    // - Requires a debugger being attached, otherwise IM_DEBUG_BREAK() options will appear to crash your application.
    //   e.g. io.ConfigDebugIsDebuggerPresent = ::IsDebuggerPresent() on Win32, or refer to ImOsIsDebuggerPresent() imgui_test_engine/imgui_te_utils.cpp for a Unix compatible version).
    byte        ConfigDebugIsDebuggerPresent;   // = false          // Enable various tools calling IM_DEBUG_BREAK().

    // Tools to test correct Begin/End and BeginChild/EndChild behaviors.
    // - Presently Begin()/End() and BeginChild()/EndChild() needs to ALWAYS be called in tandem, regardless of return value of BeginXXX()
    // - This is inconsistent with other BeginXXX functions and create confusion for many users.
    // - We expect to update the API eventually. In the meanwhile we provide tools to facilitate checking user-code behavior.
    byte        ConfigDebugBeginReturnValueOnce;// = false          // First-time calls to Begin()/BeginChild() will return false. NEEDS TO BE SET AT APPLICATION BOOT TIME if you don't want to miss windows.
    byte        ConfigDebugBeginReturnValueLoop;// = false          // Some calls to Begin()/BeginChild() will return false. Will cycle through window depths then repeat. Suggested use: add "io.ConfigDebugBeginReturnValue = io.KeyShift" in your main loop then occasionally press SHIFT. Windows should be flickering while running.

    // Option to deactivate io.AddFocusEvent(false) handling.
    // - May facilitate interactions with a debugger when focus loss leads to clearing inputs data.
    // - Backends may have other side-effects on focus loss, so this will reduce side-effects but not necessary remove all of them.
    byte        ConfigDebugIgnoreFocusLoss;     // = false          // Ignore io.AddFocusEvent(false), consequently not calling io.ClearInputKeys() in input processing.

    // Option to audit .ini data
    byte        ConfigDebugIniSettings;         // = false          // Save .ini data with extra comments (particularly helpful for Docking, but makes saving slower)

    //------------------------------------------------------------------
    // Platform Functions
    // (the imgui_impl_xxxx backend files are setting those up for you)
    //------------------------------------------------------------------

    // Optional: Platform/Renderer backend name (informational only! will be displayed in About Window) + User data for backend/wrappers to store their own stuff.
    public char* BackendPlatformName;            // = NULL
    public char* BackendRendererName;            // = NULL
    public void*       BackendPlatformUserData;        // = NULL           // User data for platform backend
    public void*       BackendRendererUserData;        // = NULL           // User data for renderer backend
    void*       BackendLanguageUserData;        // = NULL           // User data for non C++ programming language backend

    // Optional: Access OS clipboard
    // (default to use native Win32 clipboard on Windows, otherwise uses a private clipboard. Override to access OS clipboard on other architectures)
    public delegate* unmanaged[Cdecl] <void*, char*> GetClipboardTextFn;
	public delegate* unmanaged[Cdecl] <void*, char*, void> SetClipboardTextFn;
	public void*       ClipboardUserData;

    // Optional: Notify OS Input Method Editor of the screen position of your cursor for text input position (e.g. when using Japanese/Chinese IME on Windows)
    // (default to use native imm32 api on Windows)
    delegate* unmanaged[Cdecl]<void*, void*, void> SetPlatformImeDataFn; // ImGuiViewport, ImGuiPlatformImeData

    // Optional: Platform locale
    ImWchar     PlatformLocaleDecimalPoint;     // '.'              // [Experimental] Configure decimal point e.g. '.' or ',' useful for some languages (e.g. German), generally pulled from *localeconv()->decimal_point

    //------------------------------------------------------------------
    // Input - Call before calling NewFrame()
    //------------------------------------------------------------------

    // Input Functions
    
    [DllImport("cimgui")]
    static extern void  ImGuiIO_AddKeyEvent(ImGuiIO* data, ImGuiKey key, byte down);                   
    /// Queue a new key down/up event. Key should be "translated" (as in, generally ImGuiKey_A matches the key end-user would use to emit an 'A' character)
    public void  AddKeyEvent(ImGuiKey key, bool down) => ImGuiIO_AddKeyEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), key, down?(byte)1:(byte)0);              
    
    [DllImport("cimgui")]
    static extern void  ImGuiIO_AddKeyAnalogEvent(ImGuiIO* data, ImGuiKey key, byte down, float v);    
    /// Queue a new key down/up event for analog values (e.g. ImGuiKey_Gamepad_ values). Dead-zones should be handled by the backend.
    public void  AddKeyAnalogEvent(ImGuiKey key, byte down, float v) => ImGuiIO_AddKeyAnalogEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), key, down, v);
    
    [DllImport("cimgui")] 
	static extern void  ImGuiIO_AddMousePosEvent(ImGuiIO* data,float x, float y);                     // Queue a mouse position update. Use -FLT_MAX,-FLT_MAX to signify no mouse (e.g. app not focused and not hovered)
	public unsafe void AddMousePosEvent(float x, float y) => ImGuiIO_AddMousePosEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), x, y); 
	
	[DllImport("cimgui")]
	static extern void  ImGuiIO_AddMouseButtonEvent(ImGuiIO* data, int button, byte down);             // Queue a mouse button change
	public void AddMouseButtonEvent(int button, byte down) => ImGuiIO_AddMouseButtonEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), button, down);
	
	[DllImport("cimgui")]
    static extern void  ImGuiIO_AddMouseWheelEvent(ImGuiIO* data, float wheel_x, float wheel_y);       // Queue a mouse wheel update. wheel_y<0: scroll down, wheel_y>0: scroll up, wheel_x<0: scroll right, wheel_x>0: scroll left.
    public void AddMouseWheelEvent(float wheel_x, float wheel_y) => ImGuiIO_AddMouseWheelEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), wheel_x, wheel_y);
	
    [DllImport("cimgui")]
    static extern void  ImGuiIO_AddMouseSourceEvent(ImGuiIO* data, ImGuiMouseSource source);
	/// Queue a mouse source change (Mouse/TouchScreen/Pen)
    public void AddMouseSourceEvent(ImGuiMouseSource source) => ImGuiIO_AddMouseSourceEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), source);
	
	// void  AddMouseViewportEvent(ImGuiID id);                      // Queue a mouse hovered viewport. Requires backend to set ImGuiBackendFlags_HasMouseHoveredViewport to call this (for multi-viewport support).
	
	[DllImport("cimgui")]
	public static extern void  ImGuiIO_AddFocusEvent(ImGuiIO* data, byte focused);                            
	/// Queue a gain/loss of focus for the application (generally based on OS/platform focus of your window)
	public void AddFocusEvent(bool focused) => ImGuiIO_AddFocusEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), focused?(byte)1:(byte)0);

	//     void  AddInputCharacter(unsigned int c);                      // Queue a new character input
	//     void  AddInputCharacterUTF16(ImWchar16 c);                    // Queue a new character input from a UTF-16 character, it can be a surrogate
	
	[DllImport("cimgui")]
    static extern void  ImGuiIO_AddInputCharactersUTF8(ImGuiIO* data, char* str);
    /// Queue a new characters input from a UTF-8 string
    public void  AddInputCharactersUTF8(FixedString128Bytes str) => ImGuiIO_AddInputCharactersUTF8((ImGuiIO*)UnsafeUtility.AddressOf(ref this), (char*)str.GetUnsafePtr());
    
//     void  SetKeyEventNativeData(ImGuiKey key, int native_keycode, int native_scancode, int native_legacy_index = -1); // [Optional] Specify index for legacy <1.87 IsKeyXXX() functions with native indices + specify native keycode, scancode.
//     void  SetAppAcceptingEvents(byte accepting_events);           // Set master flag for accepting key/mouse/text events (default to true). Useful if you have native dialog boxes that are interrupting your application loop/refresh, and you want to disable events being queued while your app is frozen.
	
	[DllImport("cimgui")]
	static extern void  ImGuiIO_ClearEventsQueue(ImGuiIO* data);                                     
	/// Clear all incoming events.
	public void  ClearEventsQueue() => ImGuiIO_ClearEventsQueue((ImGuiIO*)UnsafeUtility.AddressOf(ref this));                                     
	
	[DllImport("cimgui")]
    static extern void  ImGuiIO_ClearInputKeys(ImGuiIO* data);                                      
    /// Clear current keyboard/mouse/gamepad state + current frame text input buffer. Equivalent to releasing all keys/buttons.
	public void  ClearInputKeys() => ImGuiIO_ClearInputKeys((ImGuiIO*)UnsafeUtility.AddressOf(ref this));                                     

    // #if !IMGUI_DISABLE_OBSOLETE_FUNCTIONS
	//    void  ClearInputCharacters();                                 // [Obsoleted in 1.89.8] Clear the current frame text input buffer. Now included within ClearInputKeys().
	// #endif

    //------------------------------------------------------------------
    // Output - Updated by NewFrame() or EndFrame()/Render()
    // (when reading from the io.WantCaptureMouse, io.WantCaptureKeyboard flags to dispatch your inputs, it is
    //  generally easier and more correct to use their state BEFORE calling NewFrame(). See FAQ for details!)
    //------------------------------------------------------------------

    byte        WantCaptureMouse;                   // Set when Dear ImGui will use mouse inputs, in this case do not dispatch them to your main game/application (either way, always pass on mouse inputs to imgui). (e.g. unclicked mouse is hovering over an imgui window, widget is active, mouse was clicked over an imgui window, etc.).
    byte        WantCaptureKeyboard;                // Set when Dear ImGui will use keyboard inputs, in this case do not dispatch them to your main game/application (either way, always pass keyboard inputs to imgui). (e.g. InputText active, or an imgui window is focused and navigation is enabled, etc.).
    byte        WantTextInput;                      // Mobile/console: when set, you may display an on-screen keyboard. This is set by Dear ImGui when it wants textual keyboard input to happen (e.g. when a InputText widget is active).
    public byte        WantSetMousePos;                    // MousePos has been altered, backend should reposition mouse on next frame. Rarely used! Set only when ImGuiConfigFlags_NavEnableSetMousePos flag is enabled.
    byte        WantSaveIniSettings;                // When manual .ini load/save is active (io.IniFilename == NULL), this will be set to notify your application that you can call SaveIniSettingsToMemory() and save yourself. Important: clear io.WantSaveIniSettings yourself after saving!
    byte        NavActive;                          // Keyboard/Gamepad navigation is currently allowed (will handle ImGuiKey_NavXXX events) = a window is focused and it doesn't use the ImGuiWindowFlags_NoNavInputs flag.
    byte        NavVisible;                         // Keyboard/Gamepad navigation is visible and allowed (will handle ImGuiKey_NavXXX events).
    public float       Framerate;                          // Estimate of application framerate (rolling average over 60 frames, based on io.DeltaTime), in frame per second. Solely for convenience. Slow applications may not want to use a moving average or may want to reset underlying buffers occasionally.
    int         MetricsRenderVertices;              // Vertices output during last call to Render()
    int         MetricsRenderIndices;               // Indices output during last call to Render() = number of triangles * 3
    int         MetricsRenderWindows;               // Number of visible windows
    int         MetricsActiveWindows;               // Number of active windows
    float2      MouseDelta;                         // Mouse delta. Note that this is zero if either current or previous position are invalid (-FLT_MAX,-FLT_MAX), so a disappearing/reappearing mouse won't have a huge delta.

    // Legacy: before 1.87, we required backend to fill io.KeyMap[] (imgui->native map) during initialization and io.KeysDown[] (native indices) every frame.
    // This is still temporarily supported as a legacy feature. However the new preferred scheme is for backend to call io.AddKeyEvent().
    //   Old (<1.87):  ImGui::IsKeyPressed(ImGui::GetIO().KeyMap[ImGuiKey_Space]) --> New (1.87+) ImGui::IsKeyPressed(ImGuiKey_Space)
#if !IMGUI_DISABLE_OBSOLETE_KEYIO
    fixed int         KeyMap[(int)ImGuiKey.COUNT];             // [LEGACY] Input: map of indices into the KeysDown[512] entries array which represent your "native" keyboard state. The first 512 are now unused and should be kept zero. Legacy backend will write into KeyMap[] using ImGuiKey_ indices which are always >512.
    fixed byte        KeysDown[(int)ImGuiKey.COUNT];           // [LEGACY] Input: Keyboard keys that are pressed (ideally left in the "native" order your engine has access to keyboard keys, so you can use your own defines/enums for keys). This used to be [512] sized. It is now ImGuiKey_COUNT to allow legacy io.KeysDown[GetKeyIndex(...)] to work without an overflow.
    fixed float       NavInputs[(int)ImGuiNavInput.COUNT];     // [LEGACY] Since 1.88, NavInputs[] was removed. Backends from 1.60 to 1.86 won't build. Feed gamepad inputs via io.AddKeyEvent() and ImGuiKey_GamepadXXX enums.
    //void*     ImeWindowHandle;                    // [Obsoleted in 1.87] Set ImGuiViewport::PlatformHandleRaw instead. Set this to your HWND to get automatic IME cursor positioning.
#endif

    //------------------------------------------------------------------
    // [Internal] Dear ImGui will maintain those fields. Forward compatibility not guaranteed!
    //------------------------------------------------------------------

    ImGuiContext* Ctx;                              // Parent UI context (needs to be set explicitly by parent).

    // Main Input State
    // (this block used to be written by backend, since 1.87 it is best to NOT write to those directly, call the AddXXX functions above instead)
    // (reading from those variables is fair game, as they are extremely unlikely to be moving anywhere)
    public float2      MousePos;                           // Mouse position, in pixels. Set to ImVec2(-FLT_MAX, -FLT_MAX) if mouse is unavailable (on another screen, etc.)
    fixed byte        MouseDown[5];                       // Mouse buttons: 0=left, 1=right, 2=middle + extras (ImGuiMouseButton_COUNT == 5). Dear ImGui mostly uses left and right buttons. Other buttons allow us to track if the mouse is being used by your application + available to user as a convenience via IsMouse** API.
    float       MouseWheel;                         // Mouse wheel Vertical: 1 unit scrolls about 5 lines text. >0 scrolls Up, <0 scrolls Down. Hold SHIFT to turn vertical scroll into horizontal scroll.
    float       MouseWheelH;                        // Mouse wheel Horizontal. >0 scrolls Left, <0 scrolls Right. Most users don't have a mouse with a horizontal wheel, may not be filled by all backends.
    ImGuiMouseSource MouseSource;                   // Mouse actual input peripheral (Mouse/TouchScreen/Pen).
    ImGuiID     MouseHoveredViewport;               // (Optional) Modify using io.AddMouseViewportEvent(). With multi-viewports: viewport the OS mouse is hovering. If possible _IGNORING_ viewports with the NoInputs flag is much better (few backends can handle that). Set io.BackendFlags |= ImGuiBackendFlags_HasMouseHoveredViewport if you can provide this info. If you don't imgui will infer the value using the rectangles and last focused time of the viewports it knows about (ignoring other OS windows).
    byte        KeyCtrl;                            // Keyboard modifier down: Control
    byte        KeyShift;                           // Keyboard modifier down: Shift
    byte        KeyAlt;                             // Keyboard modifier down: Alt
    byte        KeySuper;                           // Keyboard modifier down: Cmd/Super/Windows

    // Other state maintained from data above + IO function calls
    ImGuiModFlags KeyMods;                          // Key mods flags (any of ImGuiMod_Ctrl/ImGuiMod_Shift/ImGuiMod_Alt/ImGuiMod_Super flags, same as io.KeyCtrl/KeyShift/KeyAlt/KeySuper but merged into flags. DOES NOT CONTAINS ImGuiMod_Shortcut which is pretranslated). Read-only, updated by NewFrame()
    KeysDataArray  KeysData; // Key state for all known keys. Use IsKeyXXX() functions to access this.
    byte        WantCaptureMouseUnlessPopupClose;   // Alternative to WantCaptureMouse: (WantCaptureMouse == true && WantCaptureMouseUnlessPopupClose == false) when a click over void is expected to close a popup.
    float2      MousePosPrev;                       // Previous mouse position (note that MouseDelta is not necessary == MousePos-MousePosPrev, in case either position is invalid)
    MouseClickedPosArray      MouseClickedPos;                 // Position at time of clicking
    fixed double      MouseClickedTime[5];                // Time of last click (used to figure out double-click)
    fixed byte        MouseClicked[5];                    // Mouse button went from !Down to Down (same as MouseClickedCount[x] != 0)
    fixed byte        MouseDoubleClicked[5];              // Has mouse button been double-clicked? (same as MouseClickedCount[x] == 2)
    fixed ushort       MouseClickedCount[5];               // == 0 (not clicked), == 1 (same as MouseClicked[]), == 2 (double-clicked), == 3 (triple-clicked) etc. when going from !Down to Down
    fixed ushort       MouseClickedLastCount[5];           // Count successive number of clicks. Stays valid after mouse release. Reset after another click is done.
    fixed byte        MouseReleased[5];                   // Mouse button went from Down to !Down
    fixed byte        MouseDownOwned[5];                  // Track if button was clicked inside a dear imgui window or over void blocked by a popup. We don't request mouse capture from the application if click started outside ImGui bounds.
    fixed byte        MouseDownOwnedUnlessPopupClose[5];  // Track if button was clicked inside a dear imgui window.
    byte        MouseWheelRequestAxisSwap;          // On a non-Mac system, holding SHIFT requests WheelY to perform the equivalent of a WheelX event. On a Mac system this is already enforced by the system.
    fixed float       MouseDownDuration[5];               // Duration the mouse button has been down (0.0f == just clicked)
    fixed float       MouseDownDurationPrev[5];           // Previous time the mouse button has been down
    MouseClickedPosArray      MouseDragMaxDistanceAbs;         // Maximum distance, absolute, on each axis, of how much mouse has traveled from the clicking point
    fixed float       MouseDragMaxDistanceSqr[5];         // Squared maximum distance of how much mouse has traveled from the clicking point (used for moving thresholds)
    float       PenPressure;                        // Touch/Pen pressure (0.0f to 1.0f, should be >0.0f only when MouseDown[0] == true). Helper storage currently unused by Dear ImGui.
    byte        AppFocusLost;                       // Only modify via AddFocusEvent()
    byte        AppAcceptingEvents;                 // Only modify via SetAppAcceptingEvents()
    byte        BackendUsingLegacyKeyArrays;        // -1: unknown, 0: using AddKeyEvent(), 1: using legacy io.KeysDown[]
    byte        BackendUsingLegacyNavInputArray;    // 0: using AddKeyAnalogEvent(), 1: writing to legacy io.NavInputs[] directly
    ushort   InputQueueSurrogate;                // For AddInputCharacterUTF16()
    ImVector<ImWchar> InputQueueCharacters;         // Queue of _characters_ input (obtained by platform backend). Fill using AddInputCharacter() helper.

    // IMGUI_API   ImGuiIO();
};

// Load and rasterize multiple TTF/OTF fonts into a same texture. The font atlas will build a single texture holding:
//  - One or more fonts.
//  - Custom graphics data needed to render the shapes needed by Dear ImGui.
//  - Mouse cursor shapes for software cursor rendering (unless setting 'Flags |= NoMouseCursors' in the font atlas).
// It is the user-code responsibility to setup/build the atlas, then upload the pixel data into a texture accessible by your graphics api.
//  - Optionally, call any of the AddFont*** functions. If you don't call any, the default font embedded in the code will be loaded for you.
//  - Call GetTexDataAsAlpha8() or GetTexDataAsRGBA32() to build and retrieve pixels data.
//  - Upload the pixels data into a texture within your graphics system (see imgui_impl_xxxx.cpp examples)
//  - Call SetTexID(my_tex_id); and pass the pointer/identifier to your texture in a format natural to your graphics API.
//    This value will be passed back to you during rendering to identify the texture. Read FAQ entry about ImTextureID for more details.
// Common pitfalls:
// - If you pass a 'glyph_ranges' array to AddFont*** functions, you need to make sure that your array persist up until the
//   atlas is build (when calling GetTexData*** or Build()). We only copy the pointer, not the data.
// - Important: By default, AddFontFromMemoryTTF() takes ownership of the data. Even though we are not writing to it, we will free the pointer on destruction.
//   You can set font_cfg->FontDataOwnedByAtlas=false to keep ownership of your data and it won't be freed,
// - Even though many functions are suffixed with "TTF", OTF data is supported just as well.
// - This is an old API and it is currently awkward for those and various other reasons! We will address them in the future!
unsafe struct ImFontAtlas
{
    // ImFont*           AddFont(const ImFontConfig* font_cfg);
    [DllImport("cimgui")]
    static extern ImFont* ImFontAtlas_AddFontDefault(ImFontAtlas* data, ImFontConfig* font_cfg = null);
    public ImFont* AddFontDefault(ImFontConfig* font_cfg = null) => ImFontAtlas_AddFontDefault((ImFontAtlas*)UnsafeUtility.AddressOf(ref this), font_cfg);
    [DllImport("cimgui")]
    static extern ImFont* ImFontAtlas_AddFontFromFileTTF(ImFontAtlas* data, char* filename, float size_pixels, ImFontConfig* font_cfg = null, ImWchar* glyph_ranges = null);
    public ImFont* AddFontFromFileTTF(FixedString512Bytes filename, float size_pixels, ImFontConfig* font_cfg = null, ImWchar* glyph_ranges = null)
		=> ImFontAtlas_AddFontFromFileTTF((ImFontAtlas*)UnsafeUtility.AddressOf(ref this), (char*)filename.GetUnsafePtr(), size_pixels, font_cfg, glyph_ranges);
    // ImFont*           AddFontFromMemoryTTF(void* font_data, int font_data_size, float size_pixels, const ImFontConfig* font_cfg = NULL, const ImWchar* glyph_ranges = NULL); // Note: Transfer ownership of 'ttf_data' to ImFontAtlas! Will be deleted after destruction of the atlas. Set font_cfg->FontDataOwnedByAtlas=false to keep ownership of your data and it won't be freed.
    // ImFont*           AddFontFromMemoryCompressedTTF(const void* compressed_font_data, int compressed_font_data_size, float size_pixels, const ImFontConfig* font_cfg = NULL, const ImWchar* glyph_ranges = NULL); // 'compressed_font_data' still owned by caller. Compress with binary_to_compressed_c.cpp.
    // ImFont*           AddFontFromMemoryCompressedBase85TTF(const char* compressed_font_data_base85, float size_pixels, const ImFontConfig* font_cfg = NULL, const ImWchar* glyph_ranges = NULL);              // 'compressed_font_data_base85' still owned by caller. Compress with binary_to_compressed_c.cpp with -base85 parameter.
    // void              ClearInputData();           // Clear input data (all ImFontConfig structures including sizes, TTF data, glyph ranges, etc.) = all the data used to build the texture and fonts.
    // void              ClearTexData();             // Clear output texture data (CPU side). Saves RAM once the texture has been copied to graphics memory.
    // void              ClearFonts();               // Clear output font data (glyphs storage, UV coordinates).

    [DllImport("cimgui")]
    static extern void ImFontAtlas_Clear(ImFontAtlas* data);
    /// Clear all input and output.
    public void Clear() => ImFontAtlas_Clear((ImFontAtlas*)UnsafeUtility.AddressOf(ref this));

    // Build atlas, retrieve pixel data.
    // User is in charge of copying the pixels into graphics memory (e.g. create a texture with your engine). Then store your texture handle with SetTexID().
    // The pitch is always = Width * BytesPerPixels (1 or 4)
    // Building in RGBA32 format is provided for convenience and compatibility, but note that unless you manually manipulate or copy color data into
    // the texture (e.g. when using the AddCustomRect*** api), then the RGB pixels emitted will always be white (~75% of memory/bandwidth waste.
    // IMGUI_API bool              Build();                    // Build pixels data. This is called automatically for you by the GetTexData*** functions.
    [DllImport("cimgui")]
    static extern void              ImFontAtlas_GetTexDataAsAlpha8(ImFontAtlas* main, out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel);  // 1 byte per-pixel
	public void GetTexDataAsAlpha8(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel) 
		=> ImFontAtlas_GetTexDataAsAlpha8((ImFontAtlas*)UnsafeUtility.AddressOf(ref this), out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);

	[DllImport("cimgui")]
    static extern void              ImFontAtlas_GetTexDataAsRGBA32(ImFontAtlas* main, out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel);  // 4 bytes-per-pixel
	public void GetTexDataAsRGBA32(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel) 
		=> ImFontAtlas_GetTexDataAsRGBA32((ImFontAtlas*)UnsafeUtility.AddressOf(ref this), out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);

	internal bool                        IsBuilt() => Fonts.Size > 0 && TexReady; // Bit ambiguous: used to detect when user didn't build texture but effectively we should check TexID != 0 except that would be backend dependent...
    public void                        SetTexID(ImTextureID id)    { TexID = id; }

    //-------------------------------------------
    // Glyph Ranges
    //-------------------------------------------

    // Helpers to retrieve list of common Unicode ranges (2 value per range, values are inclusive, zero-terminated list)
    // NB: Make sure that your string are UTF-8 and NOT in your local code page.
    // Read https://github.com/ocornut/imgui/blob/master/docs/FONTS.md/#about-utf-8-encoding for details.
    // NB: Consider using ImFontGlyphRangesBuilder to build glyph ranges from textual data.
    // ImWchar*    GetGlyphRangesDefault();                // Basic Latin, Extended Latin
    // ImWchar*    GetGlyphRangesGreek();                  // Default + Greek and Coptic
    // ImWchar*    GetGlyphRangesKorean();                 // Default + Korean characters
    // ImWchar*    GetGlyphRangesJapanese();               // Default + Hiragana, Katakana, Half-Width, Selection of 2999 Ideographs
    // ImWchar*    GetGlyphRangesChineseFull();            // Default + Half-Width + Japanese Hiragana/Katakana + full set of about 21000 CJK Unified Ideographs
    // ImWchar*    GetGlyphRangesChineseSimplifiedCommon();// Default + Half-Width + Japanese Hiragana/Katakana + set of 2500 CJK Unified Ideographs for common simplified Chinese
    // ImWchar*    GetGlyphRangesCyrillic();               // Default + about 400 Cyrillic characters
    // ImWchar*    GetGlyphRangesThai();                   // Default + Thai characters
    // ImWchar*    GetGlyphRangesVietnamese();             // Default + Vietnamese characters

    //-------------------------------------------
    // [BETA] Custom Rectangles/Glyphs API
    //-------------------------------------------

    // You can request arbitrary rectangles to be packed into the atlas, for your own purposes.
    // - After calling Build(), you can query the rectangle position and render your pixels.
    // - If you render colored output, set 'atlas->TexPixelsUseColors = true' as this may help some backends decide of prefered texture format.
    // - You can also request your rectangles to be mapped as font glyph (given a font + Unicode point),
    //   so you can render e.g. custom colorful icons and use them as regular glyphs.
    // - Read docs/FONTS.md for more details about using colorful icons.
    // - Note: this API may be redesigned later in order to support multi-monitor varying DPI settings.
    // IMGUI_API int               AddCustomRectRegular(int width, int height);
    // IMGUI_API int               AddCustomRectFontGlyph(ImFont* font, ImWchar id, int width, int height, float advance_x, const ImVec2& offset = ImVec2(0, 0));
    // ImFontAtlasCustomRect*      GetCustomRectByIndex(int index) { IM_ASSERT(index >= 0); return &CustomRects[index]; }

    // [Internal]
    // IMGUI_API void              CalcCustomRectUV(const ImFontAtlasCustomRect* rect, ImVec2* out_uv_min, ImVec2* out_uv_max) const;
    // IMGUI_API bool              GetMouseCursorTexData(ImGuiMouseCursor cursor, ImVec2* out_offset, ImVec2* out_size, ImVec2 out_uv_border[2], ImVec2 out_uv_fill[2]);

    //-------------------------------------------
    // Members
    //-------------------------------------------

    ImFontAtlasFlags            Flags;              // Build flags (see )
    ImTextureID                 TexID;              // User data to refer to the texture once it has been uploaded to user's graphic systems. It is passed back to you during rendering via the ImDrawCmd structure.
    int                         TexDesiredWidth;    // Texture width desired by user before Build(). Must be a power-of-two. If have many glyphs your graphics API have texture size restrictions you may want to increase texture width to decrease height.
    int                         TexGlyphPadding;    // Padding between glyphs within texture in pixels. Defaults to 1. If your rendering method doesn't rely on bilinear filtering you may set this to 0 (will also need to set AntiAliasedLinesUseTex = false).
    bool                        Locked;             // Marked as Locked by ImGui::NewFrame() so attempt to modify the atlas will assert.
    void*                       UserData;           // Store your own atlas related user-data (if e.g. you have multiple font atlas).

    // [Internal]
    // NB: Access texture data via GetTexData*() calls! Which will setup a default font for you.
    bool                        TexReady;           // Set when texture was built matching current font input
    bool                        TexPixelsUseColors; // Tell whether our texture data is known to use colors (rather than just alpha channel), in order to help backend select a format.
    byte*              TexPixelsAlpha8;    // 1 component per pixel, each component is unsigned 8-bit. Total size = TexWidth * TexHeight
    uint*               TexPixelsRGBA32;    // 4 component per pixel, each component is unsigned 8-bit. Total size = TexWidth * TexHeight * 4
    int                         TexWidth;           // Texture width calculated during Build().
    int                         TexHeight;          // Texture height calculated during Build().
    float2                      TexUvScale;         // = (1.0f/TexWidth, 1.0f/TexHeight)
    float2                      TexUvWhitePixel;    // Texture coordinates to a white pixel
    public ImVector<Ptr<ImFont>>           Fonts;              // Hold all the fonts returned by AddFont*. Fonts[0] is the default font upon calling ImGui::NewFrame(), use ImGui::PushFont()/PopFont() to change the current font. // ImFont*
    ImVectorRaw CustomRects;    // Rectangles for packing custom texture data into the atlas. // ImFontAtlasCustomRect
    ImVector<ImFontConfig>      ConfigData;         // Configuration data
    TexUvLinesArray                      TexUvLines;  // UVs for baked anti-aliased lines

    // [Internal] Font builder
    void*      FontBuilderIO;      // Opaque interface to a font builder (default to stb_truetype, can be changed to use FreeType by defining IMGUI_ENABLE_FREETYPE). // ImFontBuilderIO
    uint                FontBuilderFlags;   // Shared flags (for all fonts) for custom font builder. THIS IS BUILD IMPLEMENTATION DEPENDENT. Per-font override is also available in ImFontConfig.

    // [Internal] Packing data
    int                         PackIdMouseCursors; // Custom texture rectangle ID for white pixel and mouse cursors
    int                         PackIdLines;        // Custom texture rectangle ID for baked anti-aliased lines

    // [Obsolete]
    //typedef ImFontAtlasCustomRect    CustomRect;         // OBSOLETED in 1.72+
    //typedef ImFontGlyphRangesBuilder GlyphRangesBuilder; // OBSOLETED in 1.67+
    public const int IM_DRAWLIST_TEX_LINES_WIDTH_MAX = 63;
};

//-----------------------------------------------------------------------------
// [SECTION] Font API (ImFontConfig, ImFontGlyph, ImFontAtlasFlags, ImFontAtlas, ImFontGlyphRangesBuilder, ImFont)
//-----------------------------------------------------------------------------
unsafe struct ImFontConfig
{
    void*           FontData;               //          // TTF/OTF data
    int             FontDataSize;           //          // TTF/OTF data size
    bool            FontDataOwnedByAtlas;   // true     // TTF/OTF data ownership taken by the container ImFontAtlas (will delete memory itself).
    int             FontNo;                 // 0        // Index of font within TTF/OTF file
    public float           SizePixels;             //          // Size in pixels for rasterizer (more or less maps to the resulting font height).
    int             OversampleH;            // 2        // Rasterize at higher quality for sub-pixel positioning. Note the difference between 2 and 3 is minimal. You can reduce this to 1 for large glyphs save memory. Read https://github.com/nothings/stb/blob/master/tests/oversample/README.md for details.
    int             OversampleV;            // 1        // Rasterize at higher quality for sub-pixel positioning. This is not really useful as we don't use sub-pixel positions on the Y axis.
    bool            PixelSnapH;             // false    // Align every glyph to pixel boundary. Useful e.g. if you are merging a non-pixel aligned font with the default font. If enabled, you can set OversampleH/V to 1.
    float2          GlyphExtraSpacing;      // 0, 0     // Extra spacing (in pixels) between glyphs. Only X axis is supported for now.
    float2          GlyphOffset;            // 0, 0     // Offset all glyphs from this font input.
    ImWchar*  GlyphRanges;            // NULL     // THE ARRAY DATA NEEDS TO PERSIST AS LONG AS THE FONT IS ALIVE. Pointer to a user-provided list of Unicode range (2 value per range, values are inclusive, zero-terminated list).
    float           GlyphMinAdvanceX;       // 0        // Minimum AdvanceX for glyphs, set Min to align font icons, set both Min/Max to enforce mono-space font
    float           GlyphMaxAdvanceX;       // FLT_MAX  // Maximum AdvanceX for glyphs
    bool            MergeMode;              // false    // Merge into previous ImFont, so you can combine multiple inputs font into one ImFont (e.g. ASCII font + icons + Japanese glyphs). You may want to use GlyphOffset.y when merge font of different heights.
    uint    FontBuilderFlags;       // 0        // Settings for custom font builder. THIS IS BUILDER IMPLEMENTATION DEPENDENT. Leave as zero if unsure.
    float           RasterizerMultiply;     // 1.0f     // Linearly brighten (>1.0f) or darken (<1.0f) font output. Brightening small fonts may be a good workaround to make them more readable. This is a silly thing we may remove in the future.
    float           RasterizerDensity;      // 1.0f     // DPI scale for rasterization, not altering other font metrics: make it easy to swap between e.g. a 100% and a 400% fonts for a zooming display. IMPORTANT: If you increase this it is expected that you increase font scale accordingly, otherwise quality may look lowered.
    ImWchar         EllipsisChar;           // -1       // Explicitly specify unicode codepoint of ellipsis character. When fonts are being merged first specified ellipsis will be used.

    // [Internal]
    fixed char            Name[40];               // Name (strictly to ease debugging)
    ImFont*         DstFont;

    // IMGUI_API ImFontConfig();
    
    public static unsafe ImFontConfig DefaultFontConfig()
    {
	    ImFontConfig config = new ImFontConfig
	    {
		    FontDataOwnedByAtlas = true,
		    OversampleH = 2,
		    OversampleV = 1,
		    GlyphMaxAdvanceX = float.MaxValue,
		    RasterizerMultiply = 5.0f,
		    RasterizerDensity = 5.0f,
		    EllipsisChar = new ImWchar{Value = (uint)(sizeof(ImWchar) - 1)},
	    };
		return config;
	}
};

// Font runtime data and rendering
// ImFontAtlas automatically loads a default embedded font for you when you call GetTexDataAsAlpha8() or GetTexDataAsRGBA32().
unsafe struct ImFont
{
    // Members: Hot ~20/24 bytes (for CalcTextSize)
    ImVector<float>             IndexAdvanceX;      // 12-16 // out //            // Sparse. Glyphs->AdvanceX in a directly indexable way (cache-friendly for CalcTextSize functions which only this this info, and are often bottleneck in large UI).
    float                       FallbackAdvanceX;   // 4     // out // = FallbackGlyph->AdvanceX
    float                       FontSize;           // 4     // in  //            // Height of characters/line, set during loading (don't change after loading)

    // Members: Hot ~28/40 bytes (for CalcTextSize + render loop)
    ImVector<ImWchar>           IndexLookup;        // 12-16 // out //            // Sparse. Index glyphs by Unicode code-point.
    ImVectorRaw       Glyphs;             // 12-16 // out //            // All glyphs. // ImFontGlyph
    void*          FallbackGlyph;      // 4-8   // out // = FindGlyph(FontFallbackChar) // ImFontGlyph

    // Members: Cold ~32/40 bytes
    ImFontAtlas*                ContainerAtlas;     // 4-8   // out //            // What we has been loaded into
    void*         ConfigData;         // 4-8   // in  //            // Pointer within ContainerAtlas->ConfigData // ImFontConfig
    short                       ConfigDataCount;    // 2     // in  // ~ 1        // Number of ImFontConfig involved in creating this font. Bigger than 1 when merging multiple font sources into one ImFont.
    ImWchar                     FallbackChar;       // 2     // out // = FFFD/'?' // Character used if a glyph isn't found.
    ImWchar                     EllipsisChar;       // 2     // out // = '...'/'.'// Character used for ellipsis rendering.
    short                       EllipsisCharCount;  // 1     // out // 1 or 3
    float                       EllipsisWidth;      // 4     // out               // Width
    float                       EllipsisCharStep;   // 4     // out               // Step between characters when EllipsisCount > 0
    bool                        DirtyLookupTables;  // 1     // out //
    float                       Scale;              // 4     // in  // = 1.f      // Base font scale, multiplied by the per-window font scale which you can adjust with SetWindowFontScale()
    float                       Ascent, Descent;    // 4+4   // out //            // Ascent: distance from top to bottom of e.g. 'A' [0..FontSize]
    int                         MetricsTotalSurface;// 4     // out //            // Total surface in pixels to get an idea of the font rasterization/texture cost (not exact, we approximate the cost of padding between glyphs)
    fixed byte                  Used4kPagesMap[(IM_UNICODE_CODEPOINT_MAX+1)/4096/8]; // 2 bytes if ImWchar=ImWchar16, 34 bytes if ImWchar==ImWchar32. Store 1-bit for each block of 4K codepoints that has one active glyph. This is mainly used to facilitate iterations across all used codepoints.

    const int IM_UNICODE_CODEPOINT_INVALID = 0xFFFD;     // Invalid Unicode code point (standard value).
#if IMGUI_USE_WCHAR32
	const int IM_UNICODE_CODEPOINT_MAX = 0x10FFFF;   // Maximum Unicode code point supported by this build.
#else
	const int IM_UNICODE_CODEPOINT_MAX = 0xFFFF;     // Maximum Unicode code point supported by this build.
#endif
    
    // Methods
    // IMGUI_API const ImFontGlyph*FindGlyph(ImWchar c) const;
    // IMGUI_API const ImFontGlyph*FindGlyphNoFallback(ImWchar c) const;
    // float                       GetCharAdvance(ImWchar c) const     { return ((int)c < IndexAdvanceX.Size) ? IndexAdvanceX[(int)c] : FallbackAdvanceX; }
    // bool                        IsLoaded() const                    { return ContainerAtlas != NULL; }
    // const char*                 GetDebugName() const                { return ConfigData ? ConfigData->Name : "<unknown>"; }
    //
    // // 'max_width' stops rendering after a certain width (could be turned into a 2d size). FLT_MAX to disable.
    // // 'wrap_width' enable automatic word-wrapping across multiple lines to fit into given width. 0.0f to disable.
    // IMGUI_API ImVec2            CalcTextSizeA(float size, float max_width, float wrap_width, const char* text_begin, const char* text_end = NULL, const char** remaining = NULL) const; // utf8
    // IMGUI_API const char*       CalcWordWrapPositionA(float scale, const char* text, const char* text_end, float wrap_width) const;
    // IMGUI_API void              RenderChar(ImDrawList* draw_list, float size, const ImVec2& pos, ImU32 col, ImWchar c) const;
    // IMGUI_API void              RenderText(ImDrawList* draw_list, float size, const ImVec2& pos, ImU32 col, const ImVec4& clip_rect, const char* text_begin, const char* text_end, float wrap_width = 0.0f, bool cpu_fine_clip = false) const;
    //
    // // [Internal] Don't use!
    // IMGUI_API void              BuildLookupTable();
    // IMGUI_API void              ClearOutputData();
    // IMGUI_API void              GrowIndex(int new_size);
    // IMGUI_API void              AddGlyph(const ImFontConfig* src_cfg, ImWchar c, float x0, float y0, float x1, float y1, float u0, float v0, float u1, float v1, float advance_x);
    // IMGUI_API void              AddRemapChar(ImWchar dst, ImWchar src, bool overwrite_dst = true); // Makes 'dst' character/glyph points to 'src' character/glyph. Currently needs to be called AFTER fonts have been built.
    // IMGUI_API void              SetGlyphVisible(ImWchar c, bool visible);
    // IMGUI_API bool              IsGlyphRangeUnused(unsigned int c_begin, unsigned int c_last);
};


[StructLayout(LayoutKind.Explicit)]
unsafe struct TexUvLinesArray
{
	[FieldOffset(0)]
	fixed float vals[(ImFontAtlas.IM_DRAWLIST_TEX_LINES_WIDTH_MAX+1) * 4];
	public float4 this[int index] 
		=> UnsafeUtility.ArrayElementAsRef<float4>(UnsafeUtility.AddressOf(ref this), index);
}


enum ImFontAtlasFlags
{
	None               = 0,
	NoPowerOfTwoHeight = 1 << 0,   // Don't round the height to next power of two
	NoMouseCursors     = 1 << 1,   // Don't build software mouse cursors into the atlas (save a little texture memory)
	NoBakedLines       = 1 << 2,   // Don't build thick line textures into the atlas (save a little texture memory, allow support for point/nearest filtering). The AntiAliasedLinesUseTex features uses them, otherwise they will be rendered using polygons (more expensive for CPU/GPU).
};


[StructLayout(LayoutKind.Explicit)]
unsafe struct MouseClickedPosArray
{
	[FieldOffset(0)] fixed float Values[5*2];
	public ref float2 this[int index] 
		=> ref UnsafeUtility.ArrayElementAsRef<float2>(UnsafeUtility.AddressOf(ref this), index);
}

// Enumeration for AddMouseSourceEvent() actual source of Mouse Input data.
// Historically we use "Mouse" terminology everywhere to indicate pointer data, e.g. MousePos, IsMousePressed(), io.AddMousePosEvent()
// But that "Mouse" data can come from different source which occasionally may be useful for application to know about.
// You can submit a change of pointer type using io.AddMouseSourceEvent().
enum ImGuiMouseSource : int
{
	Mouse = 0,         // Input is coming from an actual mouse.
	TouchScreen,       // Input is coming from a touch screen (no hovering prior to initial press, less precise initial press aiming, dual-axis wheeling possible).
	Pen,               // Input is coming from a pressure/magnetic pen (often used in conjunction with high-sampling rates).
	COUNT
};


enum ImGuiNavInput
{
	Activate, Cancel, Input, Menu, DpadLeft, DpadRight, DpadUp, DpadDown,
	LStickLeft, LStickRight, LStickUp, LStickDown, FocusPrev, FocusNext, TweakSlow, TweakFast,
	COUNT,
};

[StructLayout(LayoutKind.Explicit)]
unsafe struct KeysDataArray {
	// ImGuiKeyData is 16 bytes. We want to store 666 keys, so we need 8KB of storage.
	[FieldOffset(0)]
	fixed ulong  KeysData0[ImGuiKeyData.SIZE];
	[FieldOffset(sizeof(ulong)*ImGuiKeyData.SIZE)]
	fixed ulong  KeysData1[ImGuiKeyData.SIZE];
	
	ref KeysDataArray this[int index] => ref UnsafeUtility.ArrayElementAsRef<KeysDataArray>(UnsafeUtility.AddressOf(ref this), index);
}

struct ImGuiKeyData
{
	public byte        Down;               // True for if key is down
	public float       DownDuration;       // Duration the key has been down (<0.0f: not pressed, 0.0f: just pressed, >0.0f: time held)
	public float       DownDurationPrev;   // Last frame duration the key has been down
	public float       AnalogValue;        // 0.0f..1.0f for gamepad values
	
	// [Internal] Prior to 1.87 we required user to fill io.KeysDown[512] using their own native index + the io.KeyMap[] array.
	// We are ditching this method but keeping a legacy path for user code doing e.g. IsKeyPressed(MY_NATIVE_KEY_CODE)
	// If you need to iterate all keys (for e.g. an input mapper) you may use NamedKey_BEGIN..NamedKey_END.
	const int NamedKey_BEGIN = 512;
	const int NamedKey_END = (int)ImGuiKey.COUNT;
	const int NamedKey_COUNT = NamedKey_END - NamedKey_BEGIN;
	
#if IMGUI_DISABLE_OBSOLETE_KEYIO
    public const int SIZE          = NamedKey_COUNT;  // Size of KeysData[]: only hold named keys
    public const int OFFSET        = NamedKey_BEGIN;  // Accesses to io.KeysData[] must use (key - KeysData_OFFSET) index.
#else
	public const int SIZE = (int)ImGuiKey.COUNT;           // Size of KeysData[]: hold legacy 0..512 keycodes + named keys
	public const int OFFSET = 0; // Accesses to io.KeysData[] must use (key - KeysData_OFFSET) index.
#endif
};

enum ImGuiKey : int
{
    // Keyboard
    None = 0,
    Tab = 512,             // == NamedKey_BEGIN
    LeftArrow,
    RightArrow,
    UpArrow,
    DownArrow,
    PageUp,
    PageDown,
    Home,
    End,
    Insert,
    Delete,
    Backspace,
    Space,
    Enter,
    Escape,
    LeftCtrl, LeftShift, LeftAlt, LeftSuper,
    RightCtrl, RightShift, RightAlt, RightSuper,
    Menu,
    N0, N1, N2, N3, N4, N5, N6, N7, N8, N9,
    A, B, C, D, E, F, G, H, I, J,
    K, L, M, N, O, P, Q, R, S, T,
    U, V, W, X, Y, Z,
    F1, F2, F3, F4, F5, F6,
    F7, F8, F9, F10, F11, F12,
    F13, F14, F15, F16, F17, F18,
    F19, F20, F21, F22, F23, F24,
    Apostrophe,        // '
    Comma,             // ,
    Minus,             // -
    Period,            // .
    Slash,             // /
    Semicolon,         // ;
    Equal,             // =
    LeftBracket,       // [
    Backslash,         // \ (this text inhibit multiline comment caused by backslash)
    RightBracket,      // ]
    GraveAccent,       // `
    CapsLock,
    ScrollLock,
    NumLock,
    PrintScreen,
    Pause,
    Keypad0, Keypad1, Keypad2, Keypad3, Keypad4,
    Keypad5, Keypad6, Keypad7, Keypad8, Keypad9,
    KeypadDecimal,
    KeypadDivide,
    KeypadMultiply,
    KeypadSubtract,
    KeypadAdd,
    KeypadEnter,
    KeypadEqual,
    AppBack,               // Available on some keyboard/mouses. Often referred as "Browser Back"
    AppForward,

    // Gamepad (some of those are analog values, 0.0f to 1.0f)                          // NAVIGATION ACTION
    // (download controller mapping PNG/PSD at http://dearimgui.com/controls_sheets)
    GamepadStart,          // Menu (Xbox)      + (Switch)   Start/Options (PS)
    GamepadBack,           // View (Xbox)      - (Switch)   Share (PS)
    GamepadFaceLeft,       // X (Xbox)         Y (Switch)   Square (PS)        // Tap: Toggle Menu. Hold: Windowing mode (Focus/Move/Resize windows)
    GamepadFaceRight,      // B (Xbox)         A (Switch)   Circle (PS)        // Cancel / Close / Exit
    GamepadFaceUp,         // Y (Xbox)         X (Switch)   Triangle (PS)      // Text Input / On-screen Keyboard
    GamepadFaceDown,       // A (Xbox)         B (Switch)   Cross (PS)         // Activate / Open / Toggle / Tweak
    GamepadDpadLeft,       // D-pad Left                                       // Move / Tweak / Resize Window (in Windowing mode)
    GamepadDpadRight,      // D-pad Right                                      // Move / Tweak / Resize Window (in Windowing mode)
    GamepadDpadUp,         // D-pad Up                                         // Move / Tweak / Resize Window (in Windowing mode)
    GamepadDpadDown,       // D-pad Down                                       // Move / Tweak / Resize Window (in Windowing mode)
    GamepadL1,             // L Bumper (Xbox)  L (Switch)   L1 (PS)            // Tweak Slower / Focus Previous (in Windowing mode)
    GamepadR1,             // R Bumper (Xbox)  R (Switch)   R1 (PS)            // Tweak Faster / Focus Next (in Windowing mode)
    GamepadL2,             // L Trig. (Xbox)   ZL (Switch)  L2 (PS) [Analog]
    GamepadR2,             // R Trig. (Xbox)   ZR (Switch)  R2 (PS) [Analog]
    GamepadL3,             // L Stick (Xbox)   L3 (Switch)  L3 (PS)
    GamepadR3,             // R Stick (Xbox)   R3 (Switch)  R3 (PS)
    GamepadLStickLeft,     // [Analog]                                         // Move Window (in Windowing mode)
    GamepadLStickRight,    // [Analog]                                         // Move Window (in Windowing mode)
    GamepadLStickUp,       // [Analog]                                         // Move Window (in Windowing mode)
    GamepadLStickDown,     // [Analog]                                         // Move Window (in Windowing mode)
    GamepadRStickLeft,     // [Analog]
    GamepadRStickRight,    // [Analog]
    GamepadRStickUp,       // [Analog]
    GamepadRStickDown,     // [Analog]

    // Aliases: Mouse Buttons (auto-submitted from AddMouseButtonEvent() calls)
    // - This is mirroring the data also written to io.MouseDown[], io.MouseWheel, in a format allowing them to be accessed via standard key API.
    MouseLeft, MouseRight, MouseMiddle, MouseX1, MouseX2, MouseWheelX, MouseWheelY,

    // [Internal] Reserved for mod storage
    ReservedForModCtrl, ReservedForModShift, ReservedForModAlt, ReservedForModSuper,
    COUNT,
};

[Flags]
enum ImGuiModFlags
{
	// Keyboard Modifiers (explicitly submitted by backend via AddKeyEvent() calls)
	// - This is mirroring the data also written to io.KeyCtrl, io.KeyShift, io.KeyAlt, io.KeySuper, in a format allowing
	//   them to be accessed via standard key API, allowing calls such as IsKeyPressed(), IsKeyReleased(), querying duration etc.
	// - Code polling every key (e.g. an interface to detect a key press for input mapping) might want to ignore those
	//   and prefer using the real keys (e.g. LeftCtrl, RightCtrl instead of ImGuiMod_Ctrl).
	// - In theory the value of keyboard modifiers should be roughly equivalent to a logical or of the equivalent left/right keys.
	//   In practice: it's complicated; mods are often provided from different sources. Keyboard layout, IME, sticky keys and
	//   backends tend to interfere and break that equivalence. The safer decision is to relay that ambiguity down to the end-user...
	None                   = 0,
	Ctrl                   = 1 << 12, // Ctrl
	Shift                  = 1 << 13, // Shift
	Alt                    = 1 << 14, // Option/Menu
	Super                  = 1 << 15, // Cmd/Super/Windows
	Shortcut               = 1 << 11, // Alias for Ctrl (non-macOS) _or_ Super (macOS).
	Mask_                  = 0xF800,  // 5-bits

#if !IMGUI_DISABLE_OBSOLETE_FUNCTIONS
	ModCtrl = Ctrl, ModShift = Shift, ModAlt = Alt, ModSuper = Super, // Renamed in 1.89
	//KeyPadEnter = KeypadEnter,              // Renamed in 1.87
#endif
};