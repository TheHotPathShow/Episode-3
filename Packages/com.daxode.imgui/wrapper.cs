#define IMGUI_DISABLE_OBSOLETE_KEYIO
#define IMGUI_DISABLE_OBSOLETE_FUNCTIONS

using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using ImGuiID = System.UInt32;
using ImWchar = System.UInt32;
using ImDrawIdx = System.UInt16;

namespace com.daxode.imgui
{

#pragma warning disable CS0414 // Field is assigned but its value is never used

	static class ImGui
	{
		public const string VERSION = "1.90.5 WIP";
		const int VERSION_NUM = 19042;
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
		static extern unsafe ImGuiID igDockSpace(ImGuiID id, float2 size, ImGuiDockNodeFlags flags, ImGuiWindowClass* windowClass);

		public static unsafe ImGuiID DockSpace(ImGuiID id, float2 size = default, ImGuiDockNodeFlags flags = 0, ImGuiWindowClass* windowClass = null)
			=> igDockSpace(id, size, flags, windowClass);

		[DllImport("cimgui")]
		static extern unsafe ImGuiID igGetID_Str(byte* strId);

		/// calculate unique ID (hash of whole ID stack + given parameter). e.g. if you want to query into ImGuiStorage yourself
		public static unsafe ImGuiID GetID(FixedString128Bytes strId)
			=> igGetID_Str(strId.GetUnsafePtr());


		[DllImport("cimgui", EntryPoint = "igPushItemWidth")]
		public static extern void PushItemWidth(float item_width); // push width of items for common large "item+label" widgets. >0.0f: width in pixels, <0.0f align xx pixels to the right of window (so -FLT_MIN always align width to the right side).

		[DllImport("cimgui", EntryPoint = "igPopItemWidth")]
		public static extern void PopItemWidth();

		public static unsafe void Begin(FixedString128Bytes anotherWindow, ImGuiWindowFlags flags = 0)
			=> Begin(anotherWindow.GetUnsafePtr(), null, flags);

		public static unsafe void Begin(FixedString128Bytes anotherWindow, ref bool b, ImGuiWindowFlags flags = 0)
			=> Begin(anotherWindow.GetUnsafePtr(), UnsafeUtility.AddressOf(ref b), flags);

		public static unsafe void Begin(NativeText anotherWindow, ref bool b, ImGuiWindowFlags flags = 0)
			=> Begin((byte*)anotherWindow.GetUnsafePtr(), UnsafeUtility.AddressOf(ref b), flags);

		[DllImport("cimgui", EntryPoint = "igBegin")]
		static extern unsafe bool Begin(byte* name, void* p_open = null, ImGuiWindowFlags flags = 0);

		[DllImport("cimgui")]
		static extern void igImage(UnityObjRef<Texture2D> user_texture_id, float2 image_size,
			float2 uv0, float2 uv1,
			float4 tint_col, float4 border_col);

		public static void Image(UnityObjRef<Texture2D> user_texture_id, float2 image_size,
			float2 uv0 = default)
		{
			igImage(UnsafeUtility.As<UnityObjRef<Texture2D>, UnityObjRef<Texture2D>>(ref user_texture_id), image_size,
				uv0, new float2(1, 1), new float4(1, 1, 1, 1), default);
		}

		public static void Image(UnityObjRef<Texture2D> user_texture_id, float2 image_size,
			float2 uv0, float2 uv1)
		{
			igImage(UnsafeUtility.As<UnityObjRef<Texture2D>, UnityObjRef<Texture2D>>(ref user_texture_id), image_size,
				uv0, uv1, new float4(1, 1, 1, 1), default);
		}

		public static void Image(UnityObjRef<Texture2D> user_texture_id, float2 image_size,
			float2 uv0, float2 uv1, float4 tint_col, float4 border_col = default)
		{
			igImage(UnsafeUtility.As<UnityObjRef<Texture2D>, UnityObjRef<Texture2D>>(ref user_texture_id), image_size,
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
		{
			FixedString128Bytes defaultFormat = "%.3g";
			return SliderFloat(f.GetUnsafePtr(), (float*)UnsafeUtility.AddressOf(ref currentVal), from, to, defaultFormat.GetUnsafePtr(), 0) > 0;
		}

		/// adjust format to decorate the value with a prefix or a suffix for in-slider labels or unit display.
		[DllImport("cimgui", EntryPoint = "igSliderFloat")]
		static extern unsafe byte SliderFloat(byte* label, float* v, float v_min, float v_max, byte* format, ImGuiSliderFlags flags = 0);

		[DllImport("cimgui")]
		static extern unsafe byte igInputText(byte* label, byte* buf, int buf_size, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* user_data);

		public static unsafe bool InputText(FixedString128Bytes label, ref FixedString512Bytes buf, ImGuiInputTextFlags flags = 0, ImGuiInputTextCallback callback = default)
			=> igInputText(label.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, flags, callback, null) > 0; // user_data is in overload to avoid unsafe parameter

		public static unsafe bool InputText(FixedString128Bytes label, ref FixedString512Bytes buf, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* userData)
			=> igInputText(label.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, flags, callback, userData) > 0;

		[DllImport("cimgui")]
		static extern unsafe byte igInputTextMultiline(byte* label, byte* buf, int buf_size, float2 size, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* user_data);

		public static unsafe bool InputTextMultiline(FixedString128Bytes label, ref FixedString512Bytes buf, float2 size = default, ImGuiInputTextFlags flags = 0, ImGuiInputTextCallback callback = default)
			=> igInputTextMultiline(label.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, size, flags, callback, null) > 0; // user_data is in overload to avoid unsafe parameter

		public static unsafe bool InputTextMultiline(FixedString128Bytes label, ref FixedString512Bytes buf, float2 size, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* userData)
			=> igInputTextMultiline(label.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, size, flags, callback, userData) > 0;

		public static unsafe bool InputTextMultiline(FixedString128Bytes label, ref NativeText buf, float2 size = default, ImGuiInputTextFlags flags = 0, ImGuiInputTextCallback callback = default)
			=> igInputTextMultiline(label.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, size, flags, callback, null) > 0; // user_data is in overload to avoid unsafe parameter

		public static unsafe bool InputTextMultiline(FixedString128Bytes label, ref NativeText buf, float2 size, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* userData)
			=> igInputTextMultiline(label.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, size, flags, callback, userData) > 0;

		[DllImport("cimgui")]
		static extern unsafe byte igInputTextWithHint(byte* label, byte* hint, byte* buf, int buf_size, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* user_data);

		public static unsafe bool InputTextWithHint(FixedString128Bytes label, FixedString128Bytes hint, ref FixedString512Bytes buf, ImGuiInputTextFlags flags = 0, ImGuiInputTextCallback callback = default)
			=> igInputTextWithHint(label.GetUnsafePtr(), hint.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, flags, callback, null) > 0; // user_data is in overload to avoid unsafe parameter

		public static unsafe bool InputTextWithHint(FixedString128Bytes label, FixedString128Bytes hint, ref FixedString512Bytes buf, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* userData)
			=> igInputTextWithHint(label.GetUnsafePtr(), hint.GetUnsafePtr(), buf.GetUnsafePtr(), buf.Capacity, flags, callback, userData) > 0;


		[DllImport("cimgui")]
		static extern byte igIsItemDeactivatedAfterEdit();

		public static bool IsItemDeactivatedAfterEdit() => igIsItemDeactivatedAfterEdit() > 0;

		public static unsafe bool ColorEdit3(FixedString128Bytes clearColor, ref float4 f, ImGuiColorEditFlags flags = 0)
			=> ColorEdit3(clearColor.GetUnsafePtr(), (float*)UnsafeUtility.AddressOf(ref f), flags);

		public static unsafe bool ColorEdit3(FixedString128Bytes clearColor, ref Color f, ImGuiColorEditFlags flags = 0)
			=> ColorEdit3(clearColor.GetUnsafePtr(), (float*)UnsafeUtility.AddressOf(ref f), flags);

		public static unsafe bool ColorEdit3(FixedString128Bytes clearColor, ref float3 f, ImGuiColorEditFlags flags = 0)
			=> ColorEdit3(clearColor.GetUnsafePtr(), (float*)UnsafeUtility.AddressOf(ref f), flags);

		[DllImport("cimgui", EntryPoint = "igColorEdit3")]
		static extern unsafe bool ColorEdit3(byte* label, float* col, ImGuiColorEditFlags flags = 0);

		public static unsafe bool ColorEdit4(FixedString128Bytes clearColor, ref float4 f, ImGuiColorEditFlags flags = 0)
			=> ColorEdit4(clearColor.GetUnsafePtr(), (float*)UnsafeUtility.AddressOf(ref f), flags);

		public static unsafe bool ColorEdit4(FixedString128Bytes clearColor, ref Color f, ImGuiColorEditFlags flags = 0)
			=> ColorEdit4(clearColor.GetUnsafePtr(), (float*)UnsafeUtility.AddressOf(ref f), flags);

		[DllImport("cimgui", EntryPoint = "igColorEdit4")]
		static extern unsafe bool ColorEdit4(byte* label, float* col, ImGuiColorEditFlags flags = 0);

		public static unsafe bool Button(FixedString128Bytes button, float2 size = default)
			=> Button(button.GetUnsafePtr(), size);

		[DllImport("cimgui", EntryPoint = "igButton")]
		static extern unsafe bool Button(byte* label, float2 size); // button

		[DllImport("cimgui", EntryPoint = "igSameLine")]
		public static extern void SameLine(float offset_from_start_x = 0.0f, float spacing = -1.0f);

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
		static extern void igPushStyleColor_U32(ImGuiCol idx, Color32 col);

		/// modify a style color. always use this if you modify the style after NewFrame().
		public static void PushStyleColor(ImGuiCol idx, Color32 col) => igPushStyleColor_U32(idx, col);

		[DllImport("cimgui")]
		static extern void igPushStyleColor_Vec4(ImGuiCol idx, float4 col);

		public static void PushStyleColor(ImGuiCol idx, float4 col) => igPushStyleColor_Vec4(idx, col);
		public static void PushStyleColor(ImGuiCol idx, Color col) => igPushStyleColor_Vec4(idx, UnsafeUtility.As<Color, float4>(ref col));

		[DllImport("cimgui")]
		static extern void igPopStyleColor(int count = 1);

		public static void PopStyleColor(int count = 1) => igPopStyleColor(count);

		[DllImport("cimgui")]
		static extern unsafe void igPushStyleVar_Float(ImGuiStyleVar idx, float val);

		/// modify a style float variable. always use this if you modify the style after NewFrame().
		public static void PushStyleVar(ImGuiStyleVar idx, float val) => igPushStyleVar_Float(idx, val);

		[DllImport("cimgui")]
		static extern unsafe void igPushStyleVar_Vec2(ImGuiStyleVar idx, float2 val);

		/// modify a style ImVec2 variable. always use this if you modify the style after NewFrame().
		public static void PushStyleVar(ImGuiStyleVar idx, float2 val) => igPushStyleVar_Vec2(idx, val);

		[DllImport("cimgui")]
		static extern unsafe void igPopStyleVar(int count = 1);

		/// Pop `count` style variable changes.
		public static void PopStyleVar(int count = 1) => igPopStyleVar(count);

		[DllImport("cimgui", EntryPoint = "igSetNextWindowSize")]
		public static extern unsafe void SetNextWindowSize(float2 size, ImGuiCond cond = 0); // set next window size. set axis to 0.0f to force an auto-fit on this axis. call before Begin()

		public static unsafe void CheckVersion()
			=> DebugCheckVersionAndDataLayout(VERSION,
				sizeof(ImGuiIO), sizeof(ImGuiStyle), sizeof(float2),
				sizeof(float4), sizeof(ImDrawVert), sizeof(ImDrawIdx));
	}

	public static class ImGuiHelper
	{
		public static unsafe bool NewFrameSafe()
		{
			// Start the Dear ImGui frame
			if (ImGui.GetCurrentContext() == null || RenderHooks.GetBackendData() == null)
				return false;

			if (RenderHooks.IsNewFrame)
			{
				RenderHooks.NewFrame();
				InputAndWindowHooks.NewFrame();
				ImGui.NewFrame();
			}

			return true;
		}
	}

	/// Callback function for ImGui::InputText()
	unsafe struct ImGuiInputTextCallback
	{
		public delegate* unmanaged[Cdecl] <ImGuiInputTextCallbackData*, int> Value;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct Ptr<T> where T : unmanaged
	{
		public T* Value;
	}
	
	unsafe partial struct ImVector<T> where T : unmanaged
	{
		public ref T this[int index] => ref UnsafeUtility.ArrayElementAsRef<T>(Data, index);
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImVectorRaw
	{
		int Size;
		int Capacity;
		public void* Data;
	}

	unsafe struct ImDrawCallback
	{
		public static delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void> ResetRenderState 
			=> (delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void>)k_resetRenderState;
		const nint k_resetRenderState = -8;
	}

	unsafe partial struct ImDrawCmd
	{
		public UnityObjRef<Texture2D> GetTexID() => TextureId;
	};
	
	public struct ImFontBuilderIO{}
	public struct ImDrawListSharedData{}
	public struct ImGuiContext{}

	unsafe partial struct ImGuiIO
	{
		//------------------------------------------------------------------
		// Input - Call before calling NewFrame()
		//------------------------------------------------------------------

		// Input Functions

		[DllImport("cimgui")]
		static extern void ImGuiIO_AddKeyEvent(ImGuiIO* data, ImGuiKey key, byte down);

		/// Queue a new key down/up event. Key should be "translated" (as in, generally ImGuiKey_A matches the key end-user would use to emit an 'A' character)
		public void AddKeyEvent(ImGuiKey key, bool down) => ImGuiIO_AddKeyEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), key, down ? (byte)1 : (byte)0);

		[DllImport("cimgui")]
		static extern void ImGuiIO_AddKeyAnalogEvent(ImGuiIO* data, ImGuiKey key, byte down, float v);

		/// Queue a new key down/up event for analog values (e.g. ImGuiKey_Gamepad_ values). Dead-zones should be handled by the backend.
		public void AddKeyAnalogEvent(ImGuiKey key, byte down, float v) => ImGuiIO_AddKeyAnalogEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), key, down, v);

		[DllImport("cimgui")]
		static extern void ImGuiIO_AddMousePosEvent(ImGuiIO* data, float x, float y); // Queue a mouse position update. Use -FLT_MAX,-FLT_MAX to signify no mouse (e.g. app not focused and not hovered)

		public unsafe void AddMousePosEvent(float x, float y) => ImGuiIO_AddMousePosEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), x, y);

		[DllImport("cimgui")]
		static extern void ImGuiIO_AddMouseButtonEvent(ImGuiIO* data, int button, byte down); // Queue a mouse button change

		public void AddMouseButtonEvent(int button, byte down) => ImGuiIO_AddMouseButtonEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), button, down);

		[DllImport("cimgui")]
		static extern void ImGuiIO_AddMouseWheelEvent(ImGuiIO* data, float wheel_x, float wheel_y); // Queue a mouse wheel update. wheel_y<0: scroll down, wheel_y>0: scroll up, wheel_x<0: scroll right, wheel_x>0: scroll left.

		public void AddMouseWheelEvent(float wheel_x, float wheel_y) => ImGuiIO_AddMouseWheelEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), wheel_x, wheel_y);

		[DllImport("cimgui")]
		static extern void ImGuiIO_AddMouseSourceEvent(ImGuiIO* data, ImGuiMouseSource source);

		/// Queue a mouse source change (Mouse/TouchScreen/Pen)
		public void AddMouseSourceEvent(ImGuiMouseSource source) => ImGuiIO_AddMouseSourceEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), source);

		// void  AddMouseViewportEvent(ImGuiID id);                      // Queue a mouse hovered viewport. Requires backend to set ImGuiBackendFlags_HasMouseHoveredViewport to call this (for multi-viewport support).

		[DllImport("cimgui")]
		public static extern void ImGuiIO_AddFocusEvent(ImGuiIO* data, byte focused);

		/// Queue a gain/loss of focus for the application (generally based on OS/platform focus of your window)
		public void AddFocusEvent(bool focused) => ImGuiIO_AddFocusEvent((ImGuiIO*)UnsafeUtility.AddressOf(ref this), focused ? (byte)1 : (byte)0);

		//     void  AddInputCharacter(unsigned int c);                      // Queue a new character input
		//     void  AddInputCharacterUTF16(ImWchar16 c);                    // Queue a new character input from a UTF-16 character, it can be a surrogate

		[DllImport("cimgui")]
		static extern void ImGuiIO_AddInputCharactersUTF8(ImGuiIO* data, char* str);

		/// Queue a new characters input from a UTF-8 string
		public void AddInputCharactersUTF8(FixedString128Bytes str) => ImGuiIO_AddInputCharactersUTF8((ImGuiIO*)UnsafeUtility.AddressOf(ref this), (char*)str.GetUnsafePtr());

//     void  SetKeyEventNativeData(ImGuiKey key, int native_keycode, int native_scancode, int native_legacy_index = -1); // [Optional] Specify index for legacy <1.87 IsKeyXXX() functions with native indices + specify native keycode, scancode.
//     void  SetAppAcceptingEvents(byte accepting_events);           // Set master flag for accepting key/mouse/text events (default to true). Useful if you have native dialog boxes that are interrupting your application loop/refresh, and you want to disable events being queued while your app is frozen.

		[DllImport("cimgui")]
		static extern void ImGuiIO_ClearEventsQueue(ImGuiIO* data);

		/// Clear all incoming events.
		public void ClearEventsQueue() => ImGuiIO_ClearEventsQueue((ImGuiIO*)UnsafeUtility.AddressOf(ref this));

		[DllImport("cimgui")]
		static extern void ImGuiIO_ClearInputKeys(ImGuiIO* data);

		/// Clear current keyboard/mouse/gamepad state + current frame text input buffer. Equivalent to releasing all keys/buttons.
		public void ClearInputKeys() => ImGuiIO_ClearInputKeys((ImGuiIO*)UnsafeUtility.AddressOf(ref this));
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
//    This value will be passed back to you during rendering to identify the texture. Read FAQ entry about UnityObjRef<Texture2D> for more details.
// Common pitfalls:
// - If you pass a 'glyph_ranges' array to AddFont*** functions, you need to make sure that your array persist up until the
//   atlas is build (when calling GetTexData*** or Build()). We only copy the pointer, not the data.
// - Important: By default, AddFontFromMemoryTTF() takes ownership of the data. Even though we are not writing to it, we will free the pointer on destruction.
//   You can set font_cfg->FontDataOwnedByAtlas=false to keep ownership of your data and it won't be freed,
// - Even though many functions are suffixed with "TTF", OTF data is supported just as well.
// - This is an old API and it is currently awkward for those and various other reasons! We will address them in the future!
	unsafe partial struct ImFontAtlas
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
		static extern void ImFontAtlas_GetTexDataAsAlpha8(ImFontAtlas* main, out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel); // 1 byte per-pixel

		public void GetTexDataAsAlpha8(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel)
			=> ImFontAtlas_GetTexDataAsAlpha8((ImFontAtlas*)UnsafeUtility.AddressOf(ref this), out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);

		[DllImport("cimgui")]
		static extern void ImFontAtlas_GetTexDataAsRGBA32(ImFontAtlas* main, out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel); // 4 bytes-per-pixel

		public void GetTexDataAsRGBA32(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel)
			=> ImFontAtlas_GetTexDataAsRGBA32((ImFontAtlas*)UnsafeUtility.AddressOf(ref this), out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);

		internal bool IsBuilt() => Fonts.Size > 0 && TexReady>0; // Bit ambiguous: used to detect when user didn't build texture but effectively we should check TexID != 0 except that would be backend dependent...

		public void SetTexID(UnityObjRef<Texture2D> id)
		{
			TexID = id;
		}

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
	};

	enum ImGuiKeyNamedKey
	{
		BEGIN = 512,
		END = (int)ImGuiKey.COUNT,
		COUNT = END - BEGIN,
	}

	enum ImGuiKeyKeysData
	{
		SIZE = ImGuiKeyNamedKey.COUNT,
		OFFSET = ImGuiKeyNamedKey.BEGIN,
	}
	
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
	}
	
	[StructLayout(LayoutKind.Explicit)]
	unsafe struct ImGuiStoragePairUnion {
		[FieldOffset(0)]
		public int val_i; 
		[FieldOffset(0)]
		public float val_f; 
		[FieldOffset(0)]
		public void* val_p;
	}
	
	[StructLayout(LayoutKind.Explicit)]
	unsafe struct ImGuiStyleModUnion {
		[FieldOffset(0)]
		public fixed int BackupInt[2]; 
		[FieldOffset(0)]
		public fixed float BackupFloat[2];
	}
}