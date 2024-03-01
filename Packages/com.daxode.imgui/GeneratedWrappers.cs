using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace com.daxode.imgui
{
	public static class ImGuiConstants
	{
#if UNITY_64
		public const int PtrSize = 64/4;
#else
		public const int PtrSize = 32/4;
#endif
	}

	[Flags]
	public enum ImDrawFlags
	{
		None = 0,
		Closed = 1 << 0,
		RoundCornersTopLeft = 1 << 4,
		RoundCornersTopRight = 1 << 5,
		RoundCornersBottomLeft = 1 << 6,
		RoundCornersBottomRight = 1 << 7,
		RoundCornersNone = 1 << 8,
		RoundCornersTop = ImDrawFlags.RoundCornersTopLeft | ImDrawFlags.RoundCornersTopRight,
		RoundCornersBottom = ImDrawFlags.RoundCornersBottomLeft | ImDrawFlags.RoundCornersBottomRight,
		RoundCornersLeft = ImDrawFlags.RoundCornersBottomLeft | ImDrawFlags.RoundCornersTopLeft,
		RoundCornersRight = ImDrawFlags.RoundCornersBottomRight | ImDrawFlags.RoundCornersTopRight,
		RoundCornersAll = ImDrawFlags.RoundCornersTopLeft | ImDrawFlags.RoundCornersTopRight | ImDrawFlags.RoundCornersBottomLeft | ImDrawFlags.RoundCornersBottomRight,
		RoundCornersDefault = ImDrawFlags.RoundCornersAll,
		RoundCornersMask = ImDrawFlags.RoundCornersAll | ImDrawFlags.RoundCornersNone,
	}

	[Flags]
	public enum ImDrawListFlags
	{
		None = 0,
		AntiAliasedLines = 1 << 0,
		AntiAliasedLinesUseTex = 1 << 1,
		AntiAliasedFill = 1 << 2,
		AllowVtxOffset = 1 << 3,
	}

	[Flags]
	public enum ImFontAtlasFlags
	{
		None = 0,
		NoPowerOfTwoHeight = 1 << 0,
		NoMouseCursors = 1 << 1,
		NoBakedLines = 1 << 2,
	}

	[Flags]
	public enum ImGuiActivateFlags
	{
		None = 0,
		PreferInput = 1 << 0,
		PreferTweak = 1 << 1,
		TryToPreserveState = 1 << 2,
		FromTabbing = 1 << 3,
		FromShortcut = 1 << 4,
	}

	public enum ImGuiAxis
	{
		None = -1,
		X = 0,
		Y = 1,
	}

	[Flags]
	public enum ImGuiBackendFlags
	{
		None = 0,
		HasGamepad = 1 << 0,
		HasMouseCursors = 1 << 1,
		HasSetMousePos = 1 << 2,
		RendererHasVtxOffset = 1 << 3,
		PlatformHasViewports = 1 << 10,
		HasMouseHoveredViewport = 1 << 11,
		RendererHasViewports = 1 << 12,
	}

	[Flags]
	public enum ImGuiButtonFlags
	{
		None = 0,
		MouseButtonLeft = 1 << 0,
		MouseButtonRight = 1 << 1,
		MouseButtonMiddle = 1 << 2,
		MouseButtonMask = ImGuiButtonFlags.MouseButtonLeft | ImGuiButtonFlags.MouseButtonRight | ImGuiButtonFlags.MouseButtonMiddle,
		MouseButtonDefault = ImGuiButtonFlags.MouseButtonLeft,
		/// <remarks> This is a private member </remarks>
		PressedOnClick = 1 << 4,
		/// <remarks> This is a private member </remarks>
		PressedOnClickRelease = 1 << 5,
		/// <remarks> This is a private member </remarks>
		PressedOnClickReleaseAnywhere = 1 << 6,
		/// <remarks> This is a private member </remarks>
		PressedOnRelease = 1 << 7,
		/// <remarks> This is a private member </remarks>
		PressedOnDoubleClick = 1 << 8,
		/// <remarks> This is a private member </remarks>
		PressedOnDragDropHold = 1 << 9,
		/// <remarks> This is a private member </remarks>
		Repeat = 1 << 10,
		/// <remarks> This is a private member </remarks>
		FlattenChildren = 1 << 11,
		/// <remarks> This is a private member </remarks>
		AllowOverlap = 1 << 12,
		/// <remarks> This is a private member </remarks>
		DontClosePopups = 1 << 13,
		/// <remarks> This is a private member </remarks>
		AlignTextBaseLine = 1 << 15,
		/// <remarks> This is a private member </remarks>
		NoKeyModifiers = 1 << 16,
		/// <remarks> This is a private member </remarks>
		NoHoldingActiveId = 1 << 17,
		/// <remarks> This is a private member </remarks>
		NoNavFocus = 1 << 18,
		/// <remarks> This is a private member </remarks>
		NoHoveredOnFocus = 1 << 19,
		/// <remarks> This is a private member </remarks>
		NoSetKeyOwner = 1 << 20,
		/// <remarks> This is a private member </remarks>
		NoTestKeyOwner = 1 << 21,
		/// <remarks> This is a private member </remarks>
		PressedOnMask = ImGuiButtonFlags.PressedOnClick | ImGuiButtonFlags.PressedOnClickRelease | ImGuiButtonFlags.PressedOnClickReleaseAnywhere | ImGuiButtonFlags.PressedOnRelease | ImGuiButtonFlags.PressedOnDoubleClick | ImGuiButtonFlags.PressedOnDragDropHold,
		/// <remarks> This is a private member </remarks>
		PressedOnDefault = ImGuiButtonFlags.PressedOnClickRelease,
	}

	[Flags]
	public enum ImGuiChildFlags
	{
		None = 0,
		Border = 1 << 0,
		AlwaysUseWindowPadding = 1 << 1,
		ResizeX = 1 << 2,
		ResizeY = 1 << 3,
		AutoResizeX = 1 << 4,
		AutoResizeY = 1 << 5,
		AlwaysAutoResize = 1 << 6,
		FrameStyle = 1 << 7,
	}

	public enum ImGuiCol
	{
		Text = 0,
		TextDisabled = 1,
		WindowBg = 2,
		ChildBg = 3,
		PopupBg = 4,
		Border = 5,
		BorderShadow = 6,
		FrameBg = 7,
		FrameBgHovered = 8,
		FrameBgActive = 9,
		TitleBg = 10,
		TitleBgActive = 11,
		TitleBgCollapsed = 12,
		MenuBarBg = 13,
		ScrollbarBg = 14,
		ScrollbarGrab = 15,
		ScrollbarGrabHovered = 16,
		ScrollbarGrabActive = 17,
		CheckMark = 18,
		SliderGrab = 19,
		SliderGrabActive = 20,
		Button = 21,
		ButtonHovered = 22,
		ButtonActive = 23,
		Header = 24,
		HeaderHovered = 25,
		HeaderActive = 26,
		Separator = 27,
		SeparatorHovered = 28,
		SeparatorActive = 29,
		ResizeGrip = 30,
		ResizeGripHovered = 31,
		ResizeGripActive = 32,
		Tab = 33,
		TabHovered = 34,
		TabActive = 35,
		TabUnfocused = 36,
		TabUnfocusedActive = 37,
		DockingPreview = 38,
		DockingEmptyBg = 39,
		PlotLines = 40,
		PlotLinesHovered = 41,
		PlotHistogram = 42,
		PlotHistogramHovered = 43,
		TableHeaderBg = 44,
		TableBorderStrong = 45,
		TableBorderLight = 46,
		TableRowBg = 47,
		TableRowBgAlt = 48,
		TextSelectedBg = 49,
		DragDropTarget = 50,
		NavHighlight = 51,
		NavWindowingHighlight = 52,
		NavWindowingDimBg = 53,
		ModalWindowDimBg = 54,
		COUNT = 55,
	}

	[Flags]
	public enum ImGuiColorEditFlags
	{
		None = 0,
		NoAlpha = 1 << 1,
		NoPicker = 1 << 2,
		NoOptions = 1 << 3,
		NoSmallPreview = 1 << 4,
		NoInputs = 1 << 5,
		NoTooltip = 1 << 6,
		NoLabel = 1 << 7,
		NoSidePreview = 1 << 8,
		NoDragDrop = 1 << 9,
		NoBorder = 1 << 10,
		AlphaBar = 1 << 16,
		AlphaPreview = 1 << 17,
		AlphaPreviewHalf = 1 << 18,
		HDR = 1 << 19,
		DisplayRGB = 1 << 20,
		DisplayHSV = 1 << 21,
		DisplayHex = 1 << 22,
		Uint8 = 1 << 23,
		Float = 1 << 24,
		PickerHueBar = 1 << 25,
		PickerHueWheel = 1 << 26,
		InputRGB = 1 << 27,
		InputHSV = 1 << 28,
		DefaultOptions = ImGuiColorEditFlags.Uint8 | ImGuiColorEditFlags.DisplayRGB | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.PickerHueBar,
		DisplayMask = ImGuiColorEditFlags.DisplayRGB | ImGuiColorEditFlags.DisplayHSV | ImGuiColorEditFlags.DisplayHex,
		DataTypeMask = ImGuiColorEditFlags.Uint8 | ImGuiColorEditFlags.Float,
		PickerMask = ImGuiColorEditFlags.PickerHueWheel | ImGuiColorEditFlags.PickerHueBar,
		InputMask = ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.InputHSV,
	}

	[Flags]
	public enum ImGuiComboFlags
	{
		None = 0,
		PopupAlignLeft = 1 << 0,
		HeightSmall = 1 << 1,
		HeightRegular = 1 << 2,
		HeightLarge = 1 << 3,
		HeightLargest = 1 << 4,
		NoArrowButton = 1 << 5,
		NoPreview = 1 << 6,
		WidthFitPreview = 1 << 7,
		HeightMask = ImGuiComboFlags.HeightSmall | ImGuiComboFlags.HeightRegular | ImGuiComboFlags.HeightLarge | ImGuiComboFlags.HeightLargest,
		/// <remarks> This is a private member </remarks>
		CustomPreview = 1 << 20,
	}

	public enum ImGuiCond
	{
		None = 0,
		Always = 1 << 0,
		Once = 1 << 1,
		FirstUseEver = 1 << 2,
		Appearing = 1 << 3,
	}

	[Flags]
	public enum ImGuiConfigFlags
	{
		None = 0,
		NavEnableKeyboard = 1 << 0,
		NavEnableGamepad = 1 << 1,
		NavEnableSetMousePos = 1 << 2,
		NavNoCaptureKeyboard = 1 << 3,
		NoMouse = 1 << 4,
		NoMouseCursorChange = 1 << 5,
		DockingEnable = 1 << 6,
		ViewportsEnable = 1 << 10,
		DpiEnableScaleViewports = 1 << 14,
		DpiEnableScaleFonts = 1 << 15,
		IsSRGB = 1 << 20,
		IsTouchScreen = 1 << 21,
	}

	public enum ImGuiContextHookType
	{
		NewFramePre = 0,
		NewFramePost = 1,
		EndFramePre = 2,
		EndFramePost = 3,
		RenderPre = 4,
		RenderPost = 5,
		Shutdown = 6,
		PendingRemoval = 7,
	}

	public enum ImGuiDataAuthority
	{
		Auto = 0,
		DockNode = 1,
		Window = 2,
	}

	public enum ImGuiDataType
	{
		S8 = 0,
		U8 = 1,
		S16 = 2,
		U16 = 3,
		S32 = 4,
		U32 = 5,
		S64 = 6,
		U64 = 7,
		Float = 8,
		Double = 9,
		COUNT = 10,
		/// <remarks> This is a private member </remarks>
		String = COUNT + 1,
		/// <remarks> This is a private member </remarks>
		Pointer = COUNT + 1+1,
		/// <remarks> This is a private member </remarks>
		ID = COUNT + 1+1+1,
	}

	[Flags]
	public enum ImGuiDebugLogFlags
	{
		None = 0,
		EventActiveId = 1 << 0,
		EventFocus = 1 << 1,
		EventPopup = 1 << 2,
		EventNav = 1 << 3,
		EventClipper = 1 << 4,
		EventSelection = 1 << 5,
		EventIO = 1 << 6,
		EventInputRouting = 1 << 7,
		EventDocking = 1 << 8,
		EventViewport = 1 << 9,
		EventMask = ImGuiDebugLogFlags.EventActiveId | ImGuiDebugLogFlags.EventFocus | ImGuiDebugLogFlags.EventPopup | ImGuiDebugLogFlags.EventNav | ImGuiDebugLogFlags.EventClipper | ImGuiDebugLogFlags.EventSelection | ImGuiDebugLogFlags.EventIO | ImGuiDebugLogFlags.EventInputRouting | ImGuiDebugLogFlags.EventDocking | ImGuiDebugLogFlags.EventViewport,
		OutputToTTY = 1 << 20,
		OutputToTestEngine = 1 << 21,
	}

	public enum ImGuiDir
	{
		None = -1,
		Left = 0,
		Right = 1,
		Up = 2,
		Down = 3,
		COUNT = 4,
	}

	[Flags]
	public enum ImGuiDockNodeFlags
	{
		None = 0,
		KeepAliveOnly = 1 << 0,
		NoDockingOverCentralNode = 1 << 2,
		PassthruCentralNode = 1 << 3,
		NoDockingSplit = 1 << 4,
		NoResize = 1 << 5,
		AutoHideTabBar = 1 << 6,
		NoUndocking = 1 << 7,
		/// <remarks> This is a private member </remarks>
		DockSpace = 1 << 10,
		/// <remarks> This is a private member </remarks>
		CentralNode = 1 << 11,
		/// <remarks> This is a private member </remarks>
		NoTabBar = 1 << 12,
		/// <remarks> This is a private member </remarks>
		HiddenTabBar = 1 << 13,
		/// <remarks> This is a private member </remarks>
		NoWindowMenuButton = 1 << 14,
		/// <remarks> This is a private member </remarks>
		NoCloseButton = 1 << 15,
		/// <remarks> This is a private member </remarks>
		NoResizeX = 1 << 16,
		/// <remarks> This is a private member </remarks>
		NoResizeY = 1 << 17,
		/// <remarks> This is a private member </remarks>
		DockedWindowsInFocusRoute = 1 << 18,
		/// <remarks> This is a private member </remarks>
		NoDockingSplitOther = 1 << 19,
		/// <remarks> This is a private member </remarks>
		NoDockingOverMe = 1 << 20,
		/// <remarks> This is a private member </remarks>
		NoDockingOverOther = 1 << 21,
		/// <remarks> This is a private member </remarks>
		NoDockingOverEmpty = 1 << 22,
		/// <remarks> This is a private member </remarks>
		NoDocking = ImGuiDockNodeFlags.NoDockingOverMe | ImGuiDockNodeFlags.NoDockingOverOther | ImGuiDockNodeFlags.NoDockingOverEmpty | ImGuiDockNodeFlags.NoDockingSplit | ImGuiDockNodeFlags.NoDockingSplitOther,
		/// <remarks> This is a private member </remarks>
		SharedFlagsInheritMask = ~0,
		/// <remarks> This is a private member </remarks>
		NoResizeFlagsMask = ImGuiDockNodeFlags.NoResize | ImGuiDockNodeFlags.NoResizeX | ImGuiDockNodeFlags.NoResizeY,
		/// <remarks> This is a private member </remarks>
		LocalFlagsTransferMask = ImGuiDockNodeFlags.NoDockingSplit | ImGuiDockNodeFlags.NoResizeFlagsMask | ImGuiDockNodeFlags.AutoHideTabBar | ImGuiDockNodeFlags.CentralNode | ImGuiDockNodeFlags.NoTabBar | ImGuiDockNodeFlags.HiddenTabBar | ImGuiDockNodeFlags.NoWindowMenuButton | ImGuiDockNodeFlags.NoCloseButton,
		/// <remarks> This is a private member </remarks>
		SavedFlagsMask = ImGuiDockNodeFlags.NoResizeFlagsMask | ImGuiDockNodeFlags.DockSpace | ImGuiDockNodeFlags.CentralNode | ImGuiDockNodeFlags.NoTabBar | ImGuiDockNodeFlags.HiddenTabBar | ImGuiDockNodeFlags.NoWindowMenuButton | ImGuiDockNodeFlags.NoCloseButton,
	}

	public enum ImGuiDockNodeState
	{
		Unknown = 0,
		HostWindowHiddenBecauseSingleWindow = 1,
		HostWindowHiddenBecauseWindowsAreResizing = 2,
		HostWindowVisible = 3,
	}

	[Flags]
	public enum ImGuiDragDropFlags
	{
		None = 0,
		SourceNoPreviewTooltip = 1 << 0,
		SourceNoDisableHover = 1 << 1,
		SourceNoHoldToOpenOthers = 1 << 2,
		SourceAllowNullID = 1 << 3,
		SourceExtern = 1 << 4,
		SourceAutoExpirePayload = 1 << 5,
		AcceptBeforeDelivery = 1 << 10,
		AcceptNoDrawDefaultRect = 1 << 11,
		AcceptNoPreviewTooltip = 1 << 12,
		AcceptPeekOnly = ImGuiDragDropFlags.AcceptBeforeDelivery | ImGuiDragDropFlags.AcceptNoDrawDefaultRect,
	}

	[Flags]
	public enum ImGuiFocusRequestFlags
	{
		None = 0,
		RestoreFocusedChild = 1 << 0,
		UnlessBelowModal = 1 << 1,
	}

	[Flags]
	public enum ImGuiFocusedFlags
	{
		None = 0,
		ChildWindows = 1 << 0,
		RootWindow = 1 << 1,
		AnyWindow = 1 << 2,
		NoPopupHierarchy = 1 << 3,
		DockHierarchy = 1 << 4,
		RootAndChildWindows = ImGuiFocusedFlags.RootWindow | ImGuiFocusedFlags.ChildWindows,
	}

	[Flags]
	public enum ImGuiHoveredFlags
	{
		None = 0,
		ChildWindows = 1 << 0,
		RootWindow = 1 << 1,
		AnyWindow = 1 << 2,
		NoPopupHierarchy = 1 << 3,
		DockHierarchy = 1 << 4,
		AllowWhenBlockedByPopup = 1 << 5,
		AllowWhenBlockedByActiveItem = 1 << 7,
		AllowWhenOverlappedByItem = 1 << 8,
		AllowWhenOverlappedByWindow = 1 << 9,
		AllowWhenDisabled = 1 << 10,
		NoNavOverride = 1 << 11,
		AllowWhenOverlapped = ImGuiHoveredFlags.AllowWhenOverlappedByItem | ImGuiHoveredFlags.AllowWhenOverlappedByWindow,
		RectOnly = ImGuiHoveredFlags.AllowWhenBlockedByPopup | ImGuiHoveredFlags.AllowWhenBlockedByActiveItem | ImGuiHoveredFlags.AllowWhenOverlapped,
		RootAndChildWindows = ImGuiHoveredFlags.RootWindow | ImGuiHoveredFlags.ChildWindows,
		ForTooltip = 1 << 12,
		Stationary = 1 << 13,
		DelayNone = 1 << 14,
		DelayShort = 1 << 15,
		DelayNormal = 1 << 16,
		NoSharedDelay = 1 << 17,
		/// <remarks> This is a private member </remarks>
		DelayMask = ImGuiHoveredFlags.DelayNone | ImGuiHoveredFlags.DelayShort | ImGuiHoveredFlags.DelayNormal | ImGuiHoveredFlags.NoSharedDelay,
		/// <remarks> This is a private member </remarks>
		AllowedMaskForIsWindowHovered = ImGuiHoveredFlags.ChildWindows | ImGuiHoveredFlags.RootWindow | ImGuiHoveredFlags.AnyWindow | ImGuiHoveredFlags.NoPopupHierarchy | ImGuiHoveredFlags.DockHierarchy | ImGuiHoveredFlags.AllowWhenBlockedByPopup | ImGuiHoveredFlags.AllowWhenBlockedByActiveItem | ImGuiHoveredFlags.ForTooltip | ImGuiHoveredFlags.Stationary,
		/// <remarks> This is a private member </remarks>
		AllowedMaskForIsItemHovered = ImGuiHoveredFlags.AllowWhenBlockedByPopup | ImGuiHoveredFlags.AllowWhenBlockedByActiveItem | ImGuiHoveredFlags.AllowWhenOverlapped | ImGuiHoveredFlags.AllowWhenDisabled | ImGuiHoveredFlags.NoNavOverride | ImGuiHoveredFlags.ForTooltip | ImGuiHoveredFlags.Stationary | ImGuiHoveredFlags.DelayMask,
	}

	public enum ImGuiInputEventType
	{
		None = 0,
		MousePos = 1,
		MouseWheel = 2,
		MouseButton = 3,
		MouseViewport = 4,
		Key = 5,
		Text = 6,
		Focus = 7,
		COUNT = 8,
	}

	[Flags]
	public enum ImGuiInputFlags
	{
		None = 0,
		Repeat = 1 << 0,
		RepeatRateDefault = 1 << 1,
		RepeatRateNavMove = 1 << 2,
		RepeatRateNavTweak = 1 << 3,
		RepeatUntilRelease = 1 << 4,
		RepeatUntilKeyModsChange = 1 << 5,
		RepeatUntilKeyModsChangeFromNone = 1 << 6,
		RepeatUntilOtherKeyPress = 1 << 7,
		CondHovered = 1 << 8,
		CondActive = 1 << 9,
		CondDefault = ImGuiInputFlags.CondHovered | ImGuiInputFlags.CondActive,
		LockThisFrame = 1 << 10,
		LockUntilRelease = 1 << 11,
		RouteFocused = 1 << 12,
		RouteGlobalLow = 1 << 13,
		RouteGlobal = 1 << 14,
		RouteGlobalHigh = 1 << 15,
		RouteAlways = 1 << 16,
		RouteUnlessBgFocused = 1 << 17,
		RepeatRateMask = ImGuiInputFlags.RepeatRateDefault | ImGuiInputFlags.RepeatRateNavMove | ImGuiInputFlags.RepeatRateNavTweak,
		RepeatUntilMask = ImGuiInputFlags.RepeatUntilRelease | ImGuiInputFlags.RepeatUntilKeyModsChange | ImGuiInputFlags.RepeatUntilKeyModsChangeFromNone | ImGuiInputFlags.RepeatUntilOtherKeyPress,
		RepeatMask = ImGuiInputFlags.Repeat | ImGuiInputFlags.RepeatRateMask | ImGuiInputFlags.RepeatUntilMask,
		CondMask = ImGuiInputFlags.CondHovered | ImGuiInputFlags.CondActive,
		RouteMask = ImGuiInputFlags.RouteFocused | ImGuiInputFlags.RouteGlobal | ImGuiInputFlags.RouteGlobalLow | ImGuiInputFlags.RouteGlobalHigh,
		SupportedByIsKeyPressed = ImGuiInputFlags.RepeatMask,
		SupportedByIsMouseClicked = ImGuiInputFlags.Repeat,
		SupportedByShortcut = ImGuiInputFlags.RepeatMask | ImGuiInputFlags.RouteMask | ImGuiInputFlags.RouteAlways | ImGuiInputFlags.RouteUnlessBgFocused,
		SupportedBySetKeyOwner = ImGuiInputFlags.LockThisFrame | ImGuiInputFlags.LockUntilRelease,
		SupportedBySetItemKeyOwner = ImGuiInputFlags.SupportedBySetKeyOwner | ImGuiInputFlags.CondMask,
	}

	public enum ImGuiInputSource
	{
		None = 0,
		Mouse = 1,
		Keyboard = 2,
		Gamepad = 3,
		Clipboard = 4,
		COUNT = 5,
	}

	[Flags]
	public enum ImGuiInputTextFlags
	{
		None = 0,
		CharsDecimal = 1 << 0,
		CharsHexadecimal = 1 << 1,
		CharsUppercase = 1 << 2,
		CharsNoBlank = 1 << 3,
		AutoSelectAll = 1 << 4,
		EnterReturnsTrue = 1 << 5,
		CallbackCompletion = 1 << 6,
		CallbackHistory = 1 << 7,
		CallbackAlways = 1 << 8,
		CallbackCharFilter = 1 << 9,
		AllowTabInput = 1 << 10,
		CtrlEnterForNewLine = 1 << 11,
		NoHorizontalScroll = 1 << 12,
		AlwaysOverwrite = 1 << 13,
		ReadOnly = 1 << 14,
		Password = 1 << 15,
		NoUndoRedo = 1 << 16,
		CharsScientific = 1 << 17,
		CallbackResize = 1 << 18,
		CallbackEdit = 1 << 19,
		EscapeClearsAll = 1 << 20,
		/// <remarks> This is a private member </remarks>
		Multiline = 1 << 26,
		/// <remarks> This is a private member </remarks>
		NoMarkEdited = 1 << 27,
		/// <remarks> This is a private member </remarks>
		MergedItem = 1 << 28,
	}

	[Flags]
	public enum ImGuiItemFlags
	{
		None = 0,
		NoTabStop = 1 << 0,
		ButtonRepeat = 1 << 1,
		Disabled = 1 << 2,
		NoNav = 1 << 3,
		NoNavDefaultFocus = 1 << 4,
		SelectableDontClosePopup = 1 << 5,
		MixedValue = 1 << 6,
		ReadOnly = 1 << 7,
		NoWindowHoverableCheck = 1 << 8,
		AllowOverlap = 1 << 9,
		Inputable = 1 << 10,
		HasSelectionUserData = 1 << 11,
	}

	[Flags]
	public enum ImGuiItemStatusFlags
	{
		None = 0,
		HoveredRect = 1 << 0,
		HasDisplayRect = 1 << 1,
		Edited = 1 << 2,
		ToggledSelection = 1 << 3,
		ToggledOpen = 1 << 4,
		HasDeactivated = 1 << 5,
		Deactivated = 1 << 6,
		HoveredWindow = 1 << 7,
		Visible = 1 << 8,
		HasClipRect = 1 << 9,
	}

	public enum ImGuiKey : int
	{
		None = 0,
		Tab = 512,
		LeftArrow = 513,
		RightArrow = 514,
		UpArrow = 515,
		DownArrow = 516,
		PageUp = 517,
		PageDown = 518,
		Home = 519,
		End = 520,
		Insert = 521,
		Delete = 522,
		Backspace = 523,
		Space = 524,
		Enter = 525,
		Escape = 526,
		LeftCtrl = 527,
		LeftShift = 528,
		LeftAlt = 529,
		LeftSuper = 530,
		RightCtrl = 531,
		RightShift = 532,
		RightAlt = 533,
		RightSuper = 534,
		Menu = 535,
		No0 = 536,
		No1 = 537,
		No2 = 538,
		No3 = 539,
		No4 = 540,
		No5 = 541,
		No6 = 542,
		No7 = 543,
		No8 = 544,
		No9 = 545,
		A = 546,
		B = 547,
		C = 548,
		D = 549,
		E = 550,
		F = 551,
		G = 552,
		H = 553,
		I = 554,
		J = 555,
		K = 556,
		L = 557,
		M = 558,
		N = 559,
		O = 560,
		P = 561,
		Q = 562,
		R = 563,
		S = 564,
		T = 565,
		U = 566,
		V = 567,
		W = 568,
		X = 569,
		Y = 570,
		Z = 571,
		F1 = 572,
		F2 = 573,
		F3 = 574,
		F4 = 575,
		F5 = 576,
		F6 = 577,
		F7 = 578,
		F8 = 579,
		F9 = 580,
		F10 = 581,
		F11 = 582,
		F12 = 583,
		F13 = 584,
		F14 = 585,
		F15 = 586,
		F16 = 587,
		F17 = 588,
		F18 = 589,
		F19 = 590,
		F20 = 591,
		F21 = 592,
		F22 = 593,
		F23 = 594,
		F24 = 595,
		Apostrophe = 596,
		Comma = 597,
		Minus = 598,
		Period = 599,
		Slash = 600,
		Semicolon = 601,
		Equal = 602,
		LeftBracket = 603,
		Backslash = 604,
		RightBracket = 605,
		GraveAccent = 606,
		CapsLock = 607,
		ScrollLock = 608,
		NumLock = 609,
		PrintScreen = 610,
		Pause = 611,
		Keypad0 = 612,
		Keypad1 = 613,
		Keypad2 = 614,
		Keypad3 = 615,
		Keypad4 = 616,
		Keypad5 = 617,
		Keypad6 = 618,
		Keypad7 = 619,
		Keypad8 = 620,
		Keypad9 = 621,
		KeypadDecimal = 622,
		KeypadDivide = 623,
		KeypadMultiply = 624,
		KeypadSubtract = 625,
		KeypadAdd = 626,
		KeypadEnter = 627,
		KeypadEqual = 628,
		AppBack = 629,
		AppForward = 630,
		GamepadStart = 631,
		GamepadBack = 632,
		GamepadFaceLeft = 633,
		GamepadFaceRight = 634,
		GamepadFaceUp = 635,
		GamepadFaceDown = 636,
		GamepadDpadLeft = 637,
		GamepadDpadRight = 638,
		GamepadDpadUp = 639,
		GamepadDpadDown = 640,
		GamepadL1 = 641,
		GamepadR1 = 642,
		GamepadL2 = 643,
		GamepadR2 = 644,
		GamepadL3 = 645,
		GamepadR3 = 646,
		GamepadLStickLeft = 647,
		GamepadLStickRight = 648,
		GamepadLStickUp = 649,
		GamepadLStickDown = 650,
		GamepadRStickLeft = 651,
		GamepadRStickRight = 652,
		GamepadRStickUp = 653,
		GamepadRStickDown = 654,
		MouseLeft = 655,
		MouseRight = 656,
		MouseMiddle = 657,
		MouseX1 = 658,
		MouseX2 = 659,
		MouseWheelX = 660,
		MouseWheelY = 661,
		ReservedForModCtrl = 662,
		ReservedForModShift = 663,
		ReservedForModAlt = 664,
		ReservedForModSuper = 665,
		COUNT = 666,
	}

	public enum ImGuiLayoutType
	{
		Horizontal = 0,
		Vertical = 1,
	}

	public enum ImGuiLocKey : int
	{
		VersionStr = 0,
		TableSizeOne = 1,
		TableSizeAllFit = 2,
		TableSizeAllDefault = 3,
		TableResetOrder = 4,
		WindowingMainMenuBar = 5,
		WindowingPopup = 6,
		WindowingUntitled = 7,
		DockingHideTabBar = 8,
		DockingHoldShiftToDock = 9,
		DockingDragToUndockOrMoveNode = 10,
		COUNT = 11,
	}

	public enum ImGuiLogType
	{
		None = 0,
		TTY = 1,
		File = 2,
		Buffer = 3,
		Clipboard = 4,
	}

	public enum ImGuiMouseButton
	{
		Left = 0,
		Right = 1,
		Middle = 2,
		COUNT = 5,
	}

	public enum ImGuiMouseCursor
	{
		None = -1,
		Arrow = 0,
		TextInput = 1,
		ResizeAll = 2,
		ResizeNS = 3,
		ResizeEW = 4,
		ResizeNESW = 5,
		ResizeNWSE = 6,
		Hand = 7,
		NotAllowed = 8,
		COUNT = 9,
	}

	public enum ImGuiMouseSource : int
	{
		Mouse = 0,
		TouchScreen = 1,
		Pen = 2,
		COUNT = 3,
	}

	[Flags]
	public enum ImGuiNavHighlightFlags
	{
		None = 0,
		Compact = 1 << 1,
		AlwaysDraw = 1 << 2,
		NoRounding = 1 << 3,
	}

	public enum ImGuiNavLayer
	{
		Main = 0,
		Menu = 1,
		COUNT = 2,
	}

	[Flags]
	public enum ImGuiNavMoveFlags
	{
		None = 0,
		LoopX = 1 << 0,
		LoopY = 1 << 1,
		WrapX = 1 << 2,
		WrapY = 1 << 3,
		WrapMask = ImGuiNavMoveFlags.LoopX | ImGuiNavMoveFlags.LoopY | ImGuiNavMoveFlags.WrapX | ImGuiNavMoveFlags.WrapY,
		AllowCurrentNavId = 1 << 4,
		AlsoScoreVisibleSet = 1 << 5,
		ScrollToEdgeY = 1 << 6,
		Forwarded = 1 << 7,
		DebugNoResult = 1 << 8,
		FocusApi = 1 << 9,
		IsTabbing = 1 << 10,
		IsPageMove = 1 << 11,
		Activate = 1 << 12,
		NoSelect = 1 << 13,
		NoSetNavHighlight = 1 << 14,
	}

	[Flags]
	public enum ImGuiNextItemDataFlags
	{
		None = 0,
		HasWidth = 1 << 0,
		HasOpen = 1 << 1,
		HasShortcut = 1 << 2,
	}

	[Flags]
	public enum ImGuiNextWindowDataFlags
	{
		None = 0,
		HasPos = 1 << 0,
		HasSize = 1 << 1,
		HasContentSize = 1 << 2,
		HasCollapsed = 1 << 3,
		HasSizeConstraint = 1 << 4,
		HasFocus = 1 << 5,
		HasBgAlpha = 1 << 6,
		HasScroll = 1 << 7,
		HasChildFlags = 1 << 8,
		HasViewport = 1 << 9,
		HasDock = 1 << 10,
		HasWindowClass = 1 << 11,
	}

	[Flags]
	public enum ImGuiOldColumnFlags
	{
		None = 0,
		NoBorder = 1 << 0,
		NoResize = 1 << 1,
		NoPreserveWidths = 1 << 2,
		NoForceWithinWindow = 1 << 3,
		GrowParentContentsSize = 1 << 4,
	}

	public enum ImGuiPlotType
	{
		Lines = 0,
		Histogram = 1,
	}

	[Flags]
	public enum ImGuiPopupFlags
	{
		None = 0,
		MouseButtonLeft = 0,
		MouseButtonRight = 1,
		MouseButtonMiddle = 2,
		MouseButtonMask = 0x1F,
		MouseButtonDefault = 1,
		NoReopen = 1 << 5,
		NoOpenOverExistingPopup = 1 << 7,
		NoOpenOverItems = 1 << 8,
		AnyPopupId = 1 << 10,
		AnyPopupLevel = 1 << 11,
		AnyPopup = ImGuiPopupFlags.AnyPopupId | ImGuiPopupFlags.AnyPopupLevel,
	}

	public enum ImGuiPopupPositionPolicy
	{
		Default = 0,
		ComboBox = 1,
		Tooltip = 2,
	}

	[Flags]
	public enum ImGuiScrollFlags
	{
		None = 0,
		KeepVisibleEdgeX = 1 << 0,
		KeepVisibleEdgeY = 1 << 1,
		KeepVisibleCenterX = 1 << 2,
		KeepVisibleCenterY = 1 << 3,
		AlwaysCenterX = 1 << 4,
		AlwaysCenterY = 1 << 5,
		NoScrollParent = 1 << 6,
		MaskX = ImGuiScrollFlags.KeepVisibleEdgeX | ImGuiScrollFlags.KeepVisibleCenterX | ImGuiScrollFlags.AlwaysCenterX,
		MaskY = ImGuiScrollFlags.KeepVisibleEdgeY | ImGuiScrollFlags.KeepVisibleCenterY | ImGuiScrollFlags.AlwaysCenterY,
	}

	[Flags]
	public enum ImGuiSelectableFlags
	{
		None = 0,
		DontClosePopups = 1 << 0,
		SpanAllColumns = 1 << 1,
		AllowDoubleClick = 1 << 2,
		Disabled = 1 << 3,
		AllowOverlap = 1 << 4,
		/// <remarks> This is a private member </remarks>
		NoHoldingActiveID = 1 << 20,
		/// <remarks> This is a private member </remarks>
		SelectOnNav = 1 << 21,
		/// <remarks> This is a private member </remarks>
		SelectOnClick = 1 << 22,
		/// <remarks> This is a private member </remarks>
		SelectOnRelease = 1 << 23,
		/// <remarks> This is a private member </remarks>
		SpanAvailWidth = 1 << 24,
		/// <remarks> This is a private member </remarks>
		SetNavIdOnHover = 1 << 25,
		/// <remarks> This is a private member </remarks>
		NoPadWithHalfSpacing = 1 << 26,
		/// <remarks> This is a private member </remarks>
		NoSetKeyOwner = 1 << 27,
	}

	[Flags]
	public enum ImGuiSeparatorFlags
	{
		None = 0,
		Horizontal = 1 << 0,
		Vertical = 1 << 1,
		SpanAllColumns = 1 << 2,
	}

	[Flags]
	public enum ImGuiSliderFlags
	{
		None = 0,
		AlwaysClamp = 1 << 4,
		Logarithmic = 1 << 5,
		NoRoundToFormat = 1 << 6,
		NoInput = 1 << 7,
		InvalidMask = 0x7000000F,
		/// <remarks> This is a private member </remarks>
		Vertical = 1 << 20,
		/// <remarks> This is a private member </remarks>
		ReadOnly = 1 << 21,
	}

	public enum ImGuiSortDirection
	{
		None = 0,
		Ascending = 1,
		Descending = 2,
	}

	public enum ImGuiStyleVar
	{
		Alpha = 0,
		DisabledAlpha = 1,
		WindowPadding = 2,
		WindowRounding = 3,
		WindowBorderSize = 4,
		WindowMinSize = 5,
		WindowTitleAlign = 6,
		ChildRounding = 7,
		ChildBorderSize = 8,
		PopupRounding = 9,
		PopupBorderSize = 10,
		FramePadding = 11,
		FrameRounding = 12,
		FrameBorderSize = 13,
		ItemSpacing = 14,
		ItemInnerSpacing = 15,
		IndentSpacing = 16,
		CellPadding = 17,
		ScrollbarSize = 18,
		ScrollbarRounding = 19,
		GrabMinSize = 20,
		GrabRounding = 21,
		TabRounding = 22,
		TabBarBorderSize = 23,
		ButtonTextAlign = 24,
		SelectableTextAlign = 25,
		SeparatorTextBorderSize = 26,
		SeparatorTextAlign = 27,
		SeparatorTextPadding = 28,
		DockingSeparatorSize = 29,
		COUNT = 30,
	}

	[Flags]
	public enum ImGuiTabBarFlags
	{
		None = 0,
		Reorderable = 1 << 0,
		AutoSelectNewTabs = 1 << 1,
		TabListPopupButton = 1 << 2,
		NoCloseWithMiddleMouseButton = 1 << 3,
		NoTabListScrollingButtons = 1 << 4,
		NoTooltip = 1 << 5,
		FittingPolicyResizeDown = 1 << 6,
		FittingPolicyScroll = 1 << 7,
		FittingPolicyMask = ImGuiTabBarFlags.FittingPolicyResizeDown | ImGuiTabBarFlags.FittingPolicyScroll,
		FittingPolicyDefault = ImGuiTabBarFlags.FittingPolicyResizeDown,
		/// <remarks> This is a private member </remarks>
		DockNode = 1 << 20,
		/// <remarks> This is a private member </remarks>
		IsFocused = 1 << 21,
		/// <remarks> This is a private member </remarks>
		SaveSettings = 1 << 22,
	}

	[Flags]
	public enum ImGuiTabItemFlags
	{
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
		/// <remarks> This is a private member </remarks>
		SectionMask = ImGuiTabItemFlags.Leading | ImGuiTabItemFlags.Trailing,
		/// <remarks> This is a private member </remarks>
		NoCloseButton = 1 << 20,
		/// <remarks> This is a private member </remarks>
		Button = 1 << 21,
		/// <remarks> This is a private member </remarks>
		Unsorted = 1 << 22,
	}

	public enum ImGuiTableBgTarget
	{
		None = 0,
		RowBg0 = 1,
		RowBg1 = 2,
		CellBg = 3,
	}

	[Flags]
	public enum ImGuiTableColumnFlags
	{
		None = 0,
		Disabled = 1 << 0,
		DefaultHide = 1 << 1,
		DefaultSort = 1 << 2,
		WidthStretch = 1 << 3,
		WidthFixed = 1 << 4,
		NoResize = 1 << 5,
		NoReorder = 1 << 6,
		NoHide = 1 << 7,
		NoClip = 1 << 8,
		NoSort = 1 << 9,
		NoSortAscending = 1 << 10,
		NoSortDescending = 1 << 11,
		NoHeaderLabel = 1 << 12,
		NoHeaderWidth = 1 << 13,
		PreferSortAscending = 1 << 14,
		PreferSortDescending = 1 << 15,
		IndentEnable = 1 << 16,
		IndentDisable = 1 << 17,
		AngledHeader = 1 << 18,
		IsEnabled = 1 << 24,
		IsVisible = 1 << 25,
		IsSorted = 1 << 26,
		IsHovered = 1 << 27,
		WidthMask = ImGuiTableColumnFlags.WidthStretch | ImGuiTableColumnFlags.WidthFixed,
		IndentMask = ImGuiTableColumnFlags.IndentEnable | ImGuiTableColumnFlags.IndentDisable,
		StatusMask = ImGuiTableColumnFlags.IsEnabled | ImGuiTableColumnFlags.IsVisible | ImGuiTableColumnFlags.IsSorted | ImGuiTableColumnFlags.IsHovered,
		NoDirectResize = 1 << 30,
	}

	[Flags]
	public enum ImGuiTableFlags
	{
		None = 0,
		Resizable = 1 << 0,
		Reorderable = 1 << 1,
		Hideable = 1 << 2,
		Sortable = 1 << 3,
		NoSavedSettings = 1 << 4,
		ContextMenuInBody = 1 << 5,
		RowBg = 1 << 6,
		BordersInnerH = 1 << 7,
		BordersOuterH = 1 << 8,
		BordersInnerV = 1 << 9,
		BordersOuterV = 1 << 10,
		BordersH = ImGuiTableFlags.BordersInnerH | ImGuiTableFlags.BordersOuterH,
		BordersV = ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.BordersOuterV,
		BordersInner = ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.BordersInnerH,
		BordersOuter = ImGuiTableFlags.BordersOuterV | ImGuiTableFlags.BordersOuterH,
		Borders = ImGuiTableFlags.BordersInner | ImGuiTableFlags.BordersOuter,
		NoBordersInBody = 1 << 11,
		NoBordersInBodyUntilResize = 1 << 12,
		SizingFixedFit = 1 << 13,
		SizingFixedSame = 2 << 13,
		SizingStretchProp = 3 << 13,
		SizingStretchSame = 4 << 13,
		NoHostExtendX = 1 << 16,
		NoHostExtendY = 1 << 17,
		NoKeepColumnsVisible = 1 << 18,
		PreciseWidths = 1 << 19,
		NoClip = 1 << 20,
		PadOuterX = 1 << 21,
		NoPadOuterX = 1 << 22,
		NoPadInnerX = 1 << 23,
		ScrollX = 1 << 24,
		ScrollY = 1 << 25,
		SortMulti = 1 << 26,
		SortTristate = 1 << 27,
		HighlightHoveredColumn = 1 << 28,
		SizingMask = ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.SizingFixedSame | ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.SizingStretchSame,
	}

	[Flags]
	public enum ImGuiTableRowFlags
	{
		None = 0,
		Headers = 1 << 0,
	}

	[Flags]
	public enum ImGuiTextFlags
	{
		None = 0,
		NoWidthForLargeClippedText = 1 << 0,
	}

	[Flags]
	public enum ImGuiTooltipFlags
	{
		None = 0,
		OverridePrevious = 1 << 1,
	}

	[Flags]
	public enum ImGuiTreeNodeFlags
	{
		None = 0,
		Selected = 1 << 0,
		Framed = 1 << 1,
		AllowOverlap = 1 << 2,
		NoTreePushOnOpen = 1 << 3,
		NoAutoOpenOnLog = 1 << 4,
		DefaultOpen = 1 << 5,
		OpenOnDoubleClick = 1 << 6,
		OpenOnArrow = 1 << 7,
		Leaf = 1 << 8,
		Bullet = 1 << 9,
		FramePadding = 1 << 10,
		SpanAvailWidth = 1 << 11,
		SpanFullWidth = 1 << 12,
		SpanAllColumns = 1 << 13,
		NavLeftJumpsBackHere = 1 << 14,
		CollapsingHeader = ImGuiTreeNodeFlags.Framed | ImGuiTreeNodeFlags.NoTreePushOnOpen | ImGuiTreeNodeFlags.NoAutoOpenOnLog,
		/// <remarks> This is a private member </remarks>
		ClipLabelForTrailingButton = 1 << 20,
		/// <remarks> This is a private member </remarks>
		UpsideDownArrow = 1 << 21,
	}

	[Flags]
	public enum ImGuiTypingSelectFlags
	{
		None = 0,
		AllowBackspace = 1 << 0,
		AllowSingleCharMode = 1 << 1,
	}

	[Flags]
	public enum ImGuiViewportFlags
	{
		None = 0,
		IsPlatformWindow = 1 << 0,
		IsPlatformMonitor = 1 << 1,
		OwnedByApp = 1 << 2,
		NoDecoration = 1 << 3,
		NoTaskBarIcon = 1 << 4,
		NoFocusOnAppearing = 1 << 5,
		NoFocusOnClick = 1 << 6,
		NoInputs = 1 << 7,
		NoRendererClear = 1 << 8,
		NoAutoMerge = 1 << 9,
		TopMost = 1 << 10,
		CanHostOtherWindows = 1 << 11,
		IsMinimized = 1 << 12,
		IsFocused = 1 << 13,
	}

	public enum ImGuiWindowDockStyleCol
	{
		Text = 0,
		Tab = 1,
		TabHovered = 2,
		TabActive = 3,
		TabUnfocused = 4,
		TabUnfocusedActive = 5,
		COUNT = 6,
	}

	[Flags]
	public enum ImGuiWindowFlags
	{
		None = 0,
		NoTitleBar = 1 << 0,
		NoResize = 1 << 1,
		NoMove = 1 << 2,
		NoScrollbar = 1 << 3,
		NoScrollWithMouse = 1 << 4,
		NoCollapse = 1 << 5,
		AlwaysAutoResize = 1 << 6,
		NoBackground = 1 << 7,
		NoSavedSettings = 1 << 8,
		NoMouseInputs = 1 << 9,
		MenuBar = 1 << 10,
		HorizontalScrollbar = 1 << 11,
		NoFocusOnAppearing = 1 << 12,
		NoBringToFrontOnFocus = 1 << 13,
		AlwaysVerticalScrollbar = 1 << 14,
		AlwaysHorizontalScrollbar = 1<< 15,
		NoNavInputs = 1 << 16,
		NoNavFocus = 1 << 17,
		UnsavedDocument = 1 << 18,
		NoDocking = 1 << 19,
		NoNav = ImGuiWindowFlags.NoNavInputs | ImGuiWindowFlags.NoNavFocus,
		NoDecoration = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoCollapse,
		NoInputs = ImGuiWindowFlags.NoMouseInputs | ImGuiWindowFlags.NoNavInputs | ImGuiWindowFlags.NoNavFocus,
		NavFlattened = 1 << 23,
		ChildWindow = 1 << 24,
		Tooltip = 1 << 25,
		Popup = 1 << 26,
		Modal = 1 << 27,
		ChildMenu = 1 << 28,
		DockNodeHost = 1 << 29,
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImBitVector
	{
		public ImVector<uint> Storage;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawChannel
	{
		public ImVector<ImDrawCmd> _CmdBuffer;
		public ImVector<ushort> _IdxBuffer;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe partial struct ImDrawCmd
	{
		public Unity.Mathematics.float4 ClipRect;
		public UnityObjRef<Texture2D> TextureId;
		public uint VtxOffset;
		public uint IdxOffset;
		public uint ElemCount;
		public delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void> UserCallback;
		public void* UserCallbackData;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawCmdHeader
	{
		public Unity.Mathematics.float4 ClipRect;
		public System.IntPtr TextureId;
		public uint VtxOffset;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawData
	{
		public byte m_Valid;
		public bool Valid => m_Valid > 0;
		public int CmdListsCount;
		public int TotalIdxCount;
		public int TotalVtxCount;
		public ImVector<Ptr<ImDrawList>> CmdLists;
		public Unity.Mathematics.float2 DisplayPos;
		public Unity.Mathematics.float2 DisplaySize;
		public Unity.Mathematics.float2 FramebufferScale;
		public ImGuiViewport* OwnerViewport;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawDataBuilder
	{
		public ImDrawDataBuilder_LayersArray<Ptr<ImDrawList>>* Layers;
		public ImVector<Ptr<ImDrawList>> LayerData1;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawDataBuilder_LayersArray<T> where T : unmanaged
	{
		public fixed byte Layers[((int)(2))*(ImGuiConstants.PtrSize)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawList
	{
		public ImVector<ImDrawCmd> CmdBuffer;
		public ImVector<ushort> IdxBuffer;
		public ImVector<ImDrawVert> VtxBuffer;
		public int Flags;
		public uint _VtxCurrentIdx;
		public ImDrawListSharedData* _Data;
		public byte* _OwnerName;
		public ImDrawVert* _VtxWritePtr;
		public ushort* _IdxWritePtr;
		public ImVector<Unity.Mathematics.float4> _ClipRectStack;
		public ImVector<System.IntPtr> _TextureIdStack;
		public ImVector<Unity.Mathematics.float2> _Path;
		public ImDrawCmdHeader _CmdHeader;
		public ImDrawListSplitter _Splitter;
		public float _FringeScale;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawListSharedData
	{
		public Unity.Mathematics.float2 TexUvWhitePixel;
		public ImFont* Font;
		public float FontSize;
		public float CurveTessellationTol;
		public float CircleSegmentMaxError;
		public Unity.Mathematics.float4 ClipRectFullscreen;
		public int InitialFlags;
		public ImVector<Unity.Mathematics.float2> TempBuffer;
		public ImDrawListSharedData_ArcFastVtxArray ArcFastVtx;
		public float ArcFastRadiusCutoff;
		public ImDrawListSharedData_CircleSegmentCountsArray CircleSegmentCounts;
		public Unity.Mathematics.float4* TexUvLines;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawListSharedData_ArcFastVtxArray
	{
		public fixed byte ArcFastVtx[((int)(48))*(8)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawListSharedData_CircleSegmentCountsArray
	{
		public fixed byte CircleSegmentCounts[((int)(64))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawListSplitter
	{
		public int _Current;
		public int _Count;
		public ImVector<ImDrawChannel> _Channels;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFont
	{
		public ImVector<float> IndexAdvanceX;
		public float FallbackAdvanceX;
		public float FontSize;
		public ImVector<ushort> IndexLookup;
		public ImVector<ImFontGlyph> Glyphs;
		public ImFontGlyph* FallbackGlyph;
		public ImFontAtlas* ContainerAtlas;
		public ImFontConfig* ConfigData;
		public short ConfigDataCount;
		public ushort FallbackChar;
		public ushort EllipsisChar;
		public short EllipsisCharCount;
		public float EllipsisWidth;
		public float EllipsisCharStep;
		public bool DirtyLookupTables;
		public float Scale;
		public float Ascent;
		public float Descent;
		public int MetricsTotalSurface;
		public ImFont_Used4kPagesMapArray Used4kPagesMap;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFont_Used4kPagesMapArray
	{
		public fixed byte Used4kPagesMap[((int)((0xFFFF+1)/4096/8))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe partial struct ImFontAtlas
	{
		public int Flags;
		public UnityObjRef<Texture2D> TexID;
		public int TexDesiredWidth;
		public int TexGlyphPadding;
		public bool Locked;
		public void* UserData;
		public bool TexReady;
		public bool TexPixelsUseColors;
		public byte* TexPixelsAlpha8;
		public uint* TexPixelsRGBA32;
		public int TexWidth;
		public int TexHeight;
		public Unity.Mathematics.float2 TexUvScale;
		public Unity.Mathematics.float2 TexUvWhitePixel;
		public ImVector<Ptr<ImFont>> Fonts;
		public ImVector<ImFontAtlasCustomRect> CustomRects;
		public ImVector<ImFontConfig> ConfigData;
		public ImFontAtlas_TexUvLinesArray TexUvLines;
		public ImFontBuilderIO* FontBuilderIO;
		public uint FontBuilderFlags;
		public int PackIdMouseCursors;
		public int PackIdLines;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFontAtlas_TexUvLinesArray
	{
		public fixed byte TexUvLines[((int)((63)+1))*(16)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFontAtlasCustomRect
	{
		public ushort Width;
		public ushort Height;
		public ushort X;
		public ushort Y;
		public uint GlyphID;
		public float GlyphAdvanceX;
		public Unity.Mathematics.float2 GlyphOffset;
		public ImFont* Font;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFontBuilderIO
	{
		public delegate* unmanaged[Cdecl]<ImFontAtlas*, bool> FontBuilder_Build;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFontConfig
	{
		public void* FontData;
		public int FontDataSize;
		public bool FontDataOwnedByAtlas;
		public int FontNo;
		public float SizePixels;
		public int OversampleH;
		public int OversampleV;
		public bool PixelSnapH;
		public Unity.Mathematics.float2 GlyphExtraSpacing;
		public Unity.Mathematics.float2 GlyphOffset;
		public ushort* GlyphRanges;
		public float GlyphMinAdvanceX;
		public float GlyphMaxAdvanceX;
		public bool MergeMode;
		public uint FontBuilderFlags;
		public float RasterizerMultiply;
		public float RasterizerDensity;
		public ushort EllipsisChar;
		public ImFontConfig_NameArray Name;
		public ImFont* DstFont;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFontConfig_NameArray
	{
		public fixed byte Name[((int)(40))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFontGlyph
	{
		public uint Colored;
		public uint Visible;
		public uint Codepoint;
		public float AdvanceX;
		public float X0;
		public float Y0;
		public float X1;
		public float Y1;
		public float U0;
		public float V0;
		public float U1;
		public float V1;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFontGlyphRangesBuilder
	{
		public ImVector<uint> UsedChars;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiColorMod
	{
		public int Col;
		public Unity.Mathematics.float4 BackupValue;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiComboPreviewData
	{
		public ImRect PreviewRect;
		public Unity.Mathematics.float2 BackupCursorPos;
		public Unity.Mathematics.float2 BackupCursorMaxPos;
		public Unity.Mathematics.float2 BackupCursorPosPrevLine;
		public float BackupPrevLineTextBaseOffset;
		public int BackupLayout;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiContext
	{
		public bool Initialized;
		public bool FontAtlasOwnedByContext;
		public ImGuiIO IO;
		public ImGuiPlatformIO PlatformIO;
		public ImGuiStyle Style;
		public int ConfigFlagsCurrFrame;
		public int ConfigFlagsLastFrame;
		public ImFont* Font;
		public float FontSize;
		public float FontBaseSize;
		public ImDrawListSharedData DrawListSharedData;
		public double Time;
		public int FrameCount;
		public int FrameCountEnded;
		public int FrameCountPlatformEnded;
		public int FrameCountRendered;
		public bool WithinFrameScope;
		public bool WithinFrameScopeWithImplicitWindow;
		public bool WithinEndChild;
		public bool GcCompactAll;
		public bool TestEngineHookItems;
		public void* TestEngine;
		public ImVector<ImGuiInputEvent> InputEventsQueue;
		public ImVector<ImGuiInputEvent> InputEventsTrail;
		public ImGuiMouseSource InputEventsNextMouseSource;
		public uint InputEventsNextEventId;
		public ImVector<Ptr<ImGuiWindow>> Windows;
		public ImVector<Ptr<ImGuiWindow>> WindowsFocusOrder;
		public ImVector<Ptr<ImGuiWindow>> WindowsTempSortBuffer;
		public ImVector<ImGuiWindowStackData> CurrentWindowStack;
		public ImGuiStorage WindowsById;
		public int WindowsActiveCount;
		public Unity.Mathematics.float2 WindowsHoverPadding;
		public uint DebugBreakInWindow;
		public ImGuiWindow* CurrentWindow;
		public ImGuiWindow* HoveredWindow;
		public ImGuiWindow* HoveredWindowUnderMovingWindow;
		public ImGuiWindow* MovingWindow;
		public ImGuiWindow* WheelingWindow;
		public Unity.Mathematics.float2 WheelingWindowRefMousePos;
		public int WheelingWindowStartFrame;
		public int WheelingWindowScrolledFrame;
		public float WheelingWindowReleaseTimer;
		public Unity.Mathematics.float2 WheelingWindowWheelRemainder;
		public Unity.Mathematics.float2 WheelingAxisAvg;
		public uint DebugHookIdInfo;
		public uint HoveredId;
		public uint HoveredIdPreviousFrame;
		public bool HoveredIdAllowOverlap;
		public bool HoveredIdDisabled;
		public float HoveredIdTimer;
		public float HoveredIdNotActiveTimer;
		public uint ActiveId;
		public uint ActiveIdIsAlive;
		public float ActiveIdTimer;
		public bool ActiveIdIsJustActivated;
		public bool ActiveIdAllowOverlap;
		public bool ActiveIdNoClearOnFocusLoss;
		public bool ActiveIdHasBeenPressedBefore;
		public bool ActiveIdHasBeenEditedBefore;
		public bool ActiveIdHasBeenEditedThisFrame;
		public bool ActiveIdFromShortcut;
		public int ActiveIdMouseButton;
		public Unity.Mathematics.float2 ActiveIdClickOffset;
		public ImGuiWindow* ActiveIdWindow;
		public ImGuiInputSource ActiveIdSource;
		public uint ActiveIdPreviousFrame;
		public bool ActiveIdPreviousFrameIsAlive;
		public bool ActiveIdPreviousFrameHasBeenEditedBefore;
		public ImGuiWindow* ActiveIdPreviousFrameWindow;
		public uint LastActiveId;
		public float LastActiveIdTimer;
		public double LastKeyModsChangeTime;
		public double LastKeyModsChangeFromNoneTime;
		public double LastKeyboardKeyPressTime;
		public ImBitArray KeysMayBeCharInput;
		public ImGuiContext_KeysOwnerDataArray KeysOwnerData;
		public ImGuiKeyRoutingTable KeysRoutingTable;
		public uint ActiveIdUsingNavDirMask;
		public bool ActiveIdUsingAllKeyboardKeys;
		public int DebugBreakInShortcutRouting;
		public uint CurrentFocusScopeId;
		public int CurrentItemFlags;
		public uint DebugLocateId;
		public ImGuiNextItemData NextItemData;
		public ImGuiLastItemData LastItemData;
		public ImGuiNextWindowData NextWindowData;
		public bool DebugShowGroupRects;
		public int DebugFlashStyleColorIdx;
		public ImVector<ImGuiColorMod> ColorStack;
		public ImVector<ImGuiStyleMod> StyleVarStack;
		public ImVector<Ptr<ImFont>> FontStack;
		public ImVector<ImGuiFocusScopeData> FocusScopeStack;
		public ImVector<int> ItemFlagsStack;
		public ImVector<ImGuiGroupData> GroupStack;
		public ImVector<ImGuiPopupData> OpenPopupStack;
		public ImVector<ImGuiPopupData> BeginPopupStack;
		public ImVector<ImGuiNavTreeNodeData> NavTreeNodeStack;
		public ImVector<Ptr<ImGuiViewportP>> Viewports;
		public float CurrentDpiScale;
		public ImGuiViewportP* CurrentViewport;
		public ImGuiViewportP* MouseViewport;
		public ImGuiViewportP* MouseLastHoveredViewport;
		public uint PlatformLastFocusedViewportId;
		public ImGuiPlatformMonitor FallbackMonitor;
		public ImRect PlatformMonitorsFullWorkRect;
		public int ViewportCreatedCount;
		public int PlatformWindowsCreatedCount;
		public int ViewportFocusedStampCount;
		public ImGuiWindow* NavWindow;
		public uint NavId;
		public uint NavFocusScopeId;
		public ImVector<ImGuiFocusScopeData> NavFocusRoute;
		public uint NavActivateId;
		public uint NavActivateDownId;
		public uint NavActivatePressedId;
		public int NavActivateFlags;
		public uint NavHighlightActivatedId;
		public float NavHighlightActivatedTimer;
		public uint NavJustMovedToId;
		public uint NavJustMovedToFocusScopeId;
		public int NavJustMovedToKeyMods;
		public uint NavNextActivateId;
		public int NavNextActivateFlags;
		public ImGuiInputSource NavInputSource;
		public ImGuiNavLayer NavLayer;
		public long NavLastValidSelectionUserData;
		public bool NavIdIsAlive;
		public bool NavMousePosDirty;
		public bool NavDisableHighlight;
		public bool NavDisableMouseHover;
		public bool NavAnyRequest;
		public bool NavInitRequest;
		public bool NavInitRequestFromMove;
		public ImGuiNavItemData NavInitResult;
		public bool NavMoveSubmitted;
		public bool NavMoveScoringItems;
		public bool NavMoveForwardToNextFrame;
		public int NavMoveFlags;
		public int NavMoveScrollFlags;
		public int NavMoveKeyMods;
		public int NavMoveDir;
		public int NavMoveDirForDebug;
		public int NavMoveClipDir;
		public ImRect NavScoringRect;
		public ImRect NavScoringNoClipRect;
		public int NavScoringDebugCount;
		public int NavTabbingDir;
		public int NavTabbingCounter;
		public ImGuiNavItemData NavMoveResultLocal;
		public ImGuiNavItemData NavMoveResultLocalVisible;
		public ImGuiNavItemData NavMoveResultOther;
		public ImGuiNavItemData NavTabbingResultFirst;
		public int ConfigNavWindowingKeyNext;
		public int ConfigNavWindowingKeyPrev;
		public ImGuiWindow* NavWindowingTarget;
		public ImGuiWindow* NavWindowingTargetAnim;
		public ImGuiWindow* NavWindowingListWindow;
		public float NavWindowingTimer;
		public float NavWindowingHighlightAlpha;
		public bool NavWindowingToggleLayer;
		public ImGuiKey NavWindowingToggleKey;
		public Unity.Mathematics.float2 NavWindowingAccumDeltaPos;
		public Unity.Mathematics.float2 NavWindowingAccumDeltaSize;
		public float DimBgRatio;
		public bool DragDropActive;
		public bool DragDropWithinSource;
		public bool DragDropWithinTarget;
		public int DragDropSourceFlags;
		public int DragDropSourceFrameCount;
		public int DragDropMouseButton;
		public ImGuiPayload DragDropPayload;
		public ImRect DragDropTargetRect;
		public ImRect DragDropTargetClipRect;
		public uint DragDropTargetId;
		public int DragDropAcceptFlags;
		public float DragDropAcceptIdCurrRectSurface;
		public uint DragDropAcceptIdCurr;
		public uint DragDropAcceptIdPrev;
		public int DragDropAcceptFrameCount;
		public uint DragDropHoldJustPressedId;
		public ImVector<byte> DragDropPayloadBufHeap;
		public ImGuiContext_DragDropPayloadBufLocalArray DragDropPayloadBufLocal;
		public int ClipperTempDataStacked;
		public ImVector<ImGuiListClipperData> ClipperTempData;
		public ImGuiTable* CurrentTable;
		public uint DebugBreakInTable;
		public int TablesTempDataStacked;
		public ImVector<ImGuiTableTempData> TablesTempData;
		public ImPool<ImGuiTable> Tables;
		public ImVector<float> TablesLastTimeActive;
		public ImVector<ImDrawChannel> DrawChannelsTempMergeBuffer;
		public ImGuiTabBar* CurrentTabBar;
		public ImPool<ImGuiTabBar> TabBars;
		public ImVector<ImGuiPtrOrIndex> CurrentTabBarStack;
		public ImVector<ImGuiShrinkWidthItem> ShrinkWidthBuffer;
		public uint HoverItemDelayId;
		public uint HoverItemDelayIdPreviousFrame;
		public float HoverItemDelayTimer;
		public float HoverItemDelayClearTimer;
		public uint HoverItemUnlockedStationaryId;
		public uint HoverWindowUnlockedStationaryId;
		public int MouseCursor;
		public float MouseStationaryTimer;
		public Unity.Mathematics.float2 MouseLastValidPos;
		public ImGuiInputTextState InputTextState;
		public ImGuiInputTextDeactivatedState InputTextDeactivatedState;
		public ImFont InputTextPasswordFont;
		public uint TempInputId;
		public int BeginMenuDepth;
		public int BeginComboDepth;
		public int ColorEditOptions;
		public uint ColorEditCurrentID;
		public uint ColorEditSavedID;
		public float ColorEditSavedHue;
		public float ColorEditSavedSat;
		public uint ColorEditSavedColor;
		public Unity.Mathematics.float4 ColorPickerRef;
		public ImGuiComboPreviewData ComboPreviewData;
		public ImRect WindowResizeBorderExpectedRect;
		public bool WindowResizeRelativeMode;
		public float SliderGrabClickOffset;
		public float SliderCurrentAccum;
		public bool SliderCurrentAccumDirty;
		public bool DragCurrentAccumDirty;
		public float DragCurrentAccum;
		public float DragSpeedDefaultRatio;
		public float ScrollbarClickDeltaToGrabCenter;
		public float DisabledAlphaBackup;
		public short DisabledStackSize;
		public short LockMarkEdited;
		public short TooltipOverrideCount;
		public ImVector<byte> ClipboardHandlerData;
		public ImVector<uint> MenusIdSubmittedThisFrame;
		public ImGuiTypingSelectState TypingSelectState;
		public ImGuiPlatformImeData PlatformImeData;
		public ImGuiPlatformImeData PlatformImeDataPrev;
		public uint PlatformImeViewport;
		public ImGuiDockContext DockContext;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiDockNode*, ImGuiTabBar*, void> DockNodeWindowMenuHandler;
		public bool SettingsLoaded;
		public float SettingsDirtyTimer;
		public ImGuiTextBuffer SettingsIniData;
		public ImVector<ImGuiSettingsHandler> SettingsHandlers;
		public ImChunkStream<ImGuiWindowSettings> SettingsWindows;
		public ImChunkStream<ImGuiTableSettings> SettingsTables;
		public ImVector<ImGuiContextHook> Hooks;
		public uint HookIdNext;
		public ImGuiContext_LocalizationTableArray* LocalizationTable;
		public bool LogEnabled;
		public ImGuiLogType LogType;
		public void* LogFile;
		public ImGuiTextBuffer LogBuffer;
		public byte* LogNextPrefix;
		public byte* LogNextSuffix;
		public float LogLinePosY;
		public bool LogLineFirstItem;
		public int LogDepthRef;
		public int LogDepthToExpand;
		public int LogDepthToExpandDefault;
		public int DebugLogFlags;
		public ImGuiTextBuffer DebugLogBuf;
		public ImGuiTextIndex DebugLogIndex;
		public int DebugLogAutoDisableFlags;
		public byte DebugLogAutoDisableFrames;
		public byte DebugLocateFrames;
		public bool DebugBreakInLocateId;
		public int DebugBreakKeyChord;
		public sbyte DebugBeginReturnValueCullDepth;
		public bool DebugItemPickerActive;
		public byte DebugItemPickerMouseButton;
		public uint DebugItemPickerBreakId;
		public float DebugFlashStyleColorTime;
		public Unity.Mathematics.float4 DebugFlashStyleColorBackup;
		public ImGuiMetricsConfig DebugMetricsConfig;
		public ImGuiIDStackTool DebugIDStackTool;
		public ImGuiDebugAllocInfo DebugAllocInfo;
		public ImGuiDockNode* DebugHoveredDockNode;
		public ImGuiContext_FramerateSecPerFrameArray FramerateSecPerFrame;
		public int FramerateSecPerFrameIdx;
		public int FramerateSecPerFrameCount;
		public float FramerateSecPerFrameAccum;
		public int WantCaptureMouseNextFrame;
		public int WantCaptureKeyboardNextFrame;
		public int WantTextInputNextFrame;
		public ImVector<byte> TempBuffer;
		public ImGuiContext_TempKeychordNameArray TempKeychordName;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiContext_KeysOwnerDataArray
	{
		public fixed byte KeysOwnerData[((int)(ImGuiKeyNamedKey.COUNT))*(0+4+4+1+1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiContext_DragDropPayloadBufLocalArray
	{
		public fixed byte DragDropPayloadBufLocal[((int)(16))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiContext_LocalizationTableArray
	{
		public fixed byte LocalizationTable[((int)(ImGuiLocKey.COUNT))*(ImGuiConstants.PtrSize)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiContext_FramerateSecPerFrameArray
	{
		public fixed byte FramerateSecPerFrame[((int)(60))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiContext_TempKeychordNameArray
	{
		public fixed byte TempKeychordName[((int)(64))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiContextHook
	{
		public uint HookId;
		public ImGuiContextHookType Type;
		public uint Owner;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiContextHook*, void> Callback;
		public void* UserData;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDataTypeInfo
	{
		public System.UIntPtr Size;
		public byte* Name;
		public byte* PrintFmt;
		public byte* ScanFmt;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDataTypeTempStorage
	{
		public ImGuiDataTypeTempStorage_DataArray Data;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDataTypeTempStorage_DataArray
	{
		public fixed byte Data[((int)(8))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDataVarInfo
	{
		public int Type;
		public uint Count;
		public uint Offset;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDebugAllocEntry
	{
		public int FrameCount;
		public short AllocCount;
		public short FreeCount;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDebugAllocInfo
	{
		public int TotalAllocCount;
		public int TotalFreeCount;
		public short LastEntriesIdx;
		public ImGuiDebugAllocInfo_LastEntriesBufArray LastEntriesBuf;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDebugAllocInfo_LastEntriesBufArray
	{
		public fixed byte LastEntriesBuf[((int)(6))*(0+4+2+2)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDockContext
	{
		public ImGuiStorage Nodes;
		public ImVectorRaw Requests;
		public ImVectorRaw NodesSettings;
		public bool WantFullRebuild;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDockNode
	{
		public uint ID;
		public int SharedFlags;
		public int LocalFlags;
		public int LocalFlagsInWindows;
		public int MergedFlags;
		public ImGuiDockNodeState State;
		public ImGuiDockNode* ParentNode;
		public ImGuiDockNode_ChildNodesArray* ChildNodes;
		public ImVector<Ptr<ImGuiWindow>> Windows;
		public ImGuiTabBar* TabBar;
		public Unity.Mathematics.float2 Pos;
		public Unity.Mathematics.float2 Size;
		public Unity.Mathematics.float2 SizeRef;
		public ImGuiAxis SplitAxis;
		public ImGuiWindowClass WindowClass;
		public uint LastBgColor;
		public ImGuiWindow* HostWindow;
		public ImGuiWindow* VisibleWindow;
		public ImGuiDockNode* CentralNode;
		public ImGuiDockNode* OnlyNodeWithWindows;
		public int CountNodeWithWindows;
		public int LastFrameAlive;
		public int LastFrameActive;
		public int LastFrameFocused;
		public uint LastFocusedNodeId;
		public uint SelectedTabId;
		public uint WantCloseTabId;
		public uint RefViewportId;
		public int AuthorityForPos;
		public int AuthorityForSize;
		public int AuthorityForViewport;
		public bool IsVisible;
		public bool IsFocused;
		public bool IsBgDrawnThisFrame;
		public bool HasCloseButton;
		public bool HasWindowMenuButton;
		public bool HasCentralNodeChild;
		public bool WantCloseAll;
		public bool WantLockSizeOnce;
		public bool WantMouseMove;
		public bool WantHiddenTabBarUpdate;
		public bool WantHiddenTabBarToggle;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiDockNode_ChildNodesArray
	{
		public fixed byte ChildNodes[((int)(2))*(ImGuiConstants.PtrSize)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiFocusScopeData
	{
		public uint ID;
		public uint WindowID;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiGroupData
	{
		public uint WindowID;
		public Unity.Mathematics.float2 BackupCursorPos;
		public Unity.Mathematics.float2 BackupCursorMaxPos;
		public Unity.Mathematics.float2 BackupCursorPosPrevLine;
		public float BackupIndent;
		public float BackupGroupOffset;
		public Unity.Mathematics.float2 BackupCurrLineSize;
		public float BackupCurrLineTextBaseOffset;
		public uint BackupActiveIdIsAlive;
		public bool BackupActiveIdPreviousFrameIsAlive;
		public bool BackupHoveredIdIsAlive;
		public bool BackupIsSameLine;
		public bool EmitItem;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIDStackTool
	{
		public int LastActiveFrame;
		public int StackLevel;
		public uint QueryId;
		public ImVector<ImGuiStackLevelInfo> Results;
		public bool CopyToClipboardOnCtrlC;
		public float CopyToClipboardLastTime;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe partial struct ImGuiIO
	{
		public ImGuiConfigFlags ConfigFlags;
		public ImGuiBackendFlags BackendFlags;
		public Unity.Mathematics.float2 DisplaySize;
		public float DeltaTime;
		public float IniSavingRate;
		public byte* IniFilename;
		public byte* LogFilename;
		public void* UserData;
		public ImFontAtlas* Fonts;
		public float FontGlobalScale;
		public bool FontAllowUserScaling;
		public ImFont* FontDefault;
		public Unity.Mathematics.float2 DisplayFramebufferScale;
		public bool ConfigDockingNoSplit;
		public bool ConfigDockingWithShift;
		public bool ConfigDockingAlwaysTabBar;
		public bool ConfigDockingTransparentPayload;
		public bool ConfigViewportsNoAutoMerge;
		public bool ConfigViewportsNoTaskBarIcon;
		public bool ConfigViewportsNoDecoration;
		public bool ConfigViewportsNoDefaultParent;
		public bool MouseDrawCursor;
		public bool ConfigMacOSXBehaviors;
		public bool ConfigInputTrickleEventQueue;
		public bool ConfigInputTextCursorBlink;
		public bool ConfigInputTextEnterKeepActive;
		public bool ConfigDragClickToInputText;
		public bool ConfigWindowsResizeFromEdges;
		public bool ConfigWindowsMoveFromTitleBarOnly;
		public float ConfigMemoryCompactTimer;
		public float MouseDoubleClickTime;
		public float MouseDoubleClickMaxDist;
		public float MouseDragThreshold;
		public float KeyRepeatDelay;
		public float KeyRepeatRate;
		public bool ConfigDebugIsDebuggerPresent;
		public bool ConfigDebugBeginReturnValueOnce;
		public bool ConfigDebugBeginReturnValueLoop;
		public bool ConfigDebugIgnoreFocusLoss;
		public bool ConfigDebugIniSettings;
		public byte* BackendPlatformName;
		public byte* BackendRendererName;
		public void* BackendPlatformUserData;
		public void* BackendRendererUserData;
		public void* BackendLanguageUserData;
		public delegate* unmanaged[Cdecl]<System.IntPtr, char*> GetClipboardTextFn;
		public delegate* unmanaged[Cdecl]<System.IntPtr, byte*, void> SetClipboardTextFn;
		public void* ClipboardUserData;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, ImGuiPlatformImeData*, void> SetPlatformImeDataFn;
		public ushort PlatformLocaleDecimalPoint;
		public bool WantCaptureMouse;
		public bool WantCaptureKeyboard;
		public bool WantTextInput;
		public bool WantSetMousePos;
		public bool WantSaveIniSettings;
		public bool NavActive;
		public bool NavVisible;
		public float Framerate;
		public int MetricsRenderVertices;
		public int MetricsRenderIndices;
		public int MetricsRenderWindows;
		public int MetricsActiveWindows;
		public Unity.Mathematics.float2 MouseDelta;
		public ImGuiContext* Ctx;
		public Unity.Mathematics.float2 MousePos;
		public ImGuiIO_MouseDownArray MouseDown;
		public float MouseWheel;
		public float MouseWheelH;
		public ImGuiMouseSource MouseSource;
		public uint MouseHoveredViewport;
		public bool KeyCtrl;
		public bool KeyShift;
		public bool KeyAlt;
		public bool KeySuper;
		public int KeyMods;
		public ImGuiIO_KeysDataArray KeysData;
		public bool WantCaptureMouseUnlessPopupClose;
		public Unity.Mathematics.float2 MousePosPrev;
		public ImGuiIO_MouseClickedPosArray MouseClickedPos;
		public ImGuiIO_MouseClickedTimeArray MouseClickedTime;
		public ImGuiIO_MouseClickedArray MouseClicked;
		public ImGuiIO_MouseDoubleClickedArray MouseDoubleClicked;
		public ImGuiIO_MouseClickedCountArray MouseClickedCount;
		public ImGuiIO_MouseClickedLastCountArray MouseClickedLastCount;
		public ImGuiIO_MouseReleasedArray MouseReleased;
		public ImGuiIO_MouseDownOwnedArray MouseDownOwned;
		public ImGuiIO_MouseDownOwnedUnlessPopupCloseArray MouseDownOwnedUnlessPopupClose;
		public bool MouseWheelRequestAxisSwap;
		public ImGuiIO_MouseDownDurationArray MouseDownDuration;
		public ImGuiIO_MouseDownDurationPrevArray MouseDownDurationPrev;
		public ImGuiIO_MouseDragMaxDistanceAbsArray MouseDragMaxDistanceAbs;
		public ImGuiIO_MouseDragMaxDistanceSqrArray MouseDragMaxDistanceSqr;
		public float PenPressure;
		public bool AppFocusLost;
		public bool AppAcceptingEvents;
		public sbyte BackendUsingLegacyKeyArrays;
		public bool BackendUsingLegacyNavInputArray;
		public ushort InputQueueSurrogate;
		public ImVector<ushort> InputQueueCharacters;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDownArray
	{
		public fixed byte MouseDown[((int)(5))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_KeysDataArray
	{
		public fixed byte KeysData[((int)(ImGuiKeyKeysData.SIZE))*(0+1+4+4+4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseClickedPosArray
	{
		public fixed byte MouseClickedPos[((int)(5))*(8)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseClickedTimeArray
	{
		public fixed byte MouseClickedTime[((int)(5))*(8)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseClickedArray
	{
		public fixed byte MouseClicked[((int)(5))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDoubleClickedArray
	{
		public fixed byte MouseDoubleClicked[((int)(5))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseClickedCountArray
	{
		public fixed byte MouseClickedCount[((int)(5))*(2)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseClickedLastCountArray
	{
		public fixed byte MouseClickedLastCount[((int)(5))*(2)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseReleasedArray
	{
		public fixed byte MouseReleased[((int)(5))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDownOwnedArray
	{
		public fixed byte MouseDownOwned[((int)(5))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDownOwnedUnlessPopupCloseArray
	{
		public fixed byte MouseDownOwnedUnlessPopupClose[((int)(5))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDownDurationArray
	{
		public fixed byte MouseDownDuration[((int)(5))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDownDurationPrevArray
	{
		public fixed byte MouseDownDurationPrev[((int)(5))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDragMaxDistanceAbsArray
	{
		public fixed byte MouseDragMaxDistanceAbs[((int)(5))*(8)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDragMaxDistanceSqrArray
	{
		public fixed byte MouseDragMaxDistanceSqr[((int)(5))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEvent
	{
		public ImGuiInputEventType Type;
		public ImGuiInputSource Source;
		public uint EventId;
		public ImGuiInputEventUnion union;
		public bool AddedByTestEngine;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEventAppFocused
	{
		public bool Focused;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEventKey
	{
		public ImGuiKey Key;
		public bool Down;
		public float AnalogValue;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEventMouseButton
	{
		public int Button;
		public bool Down;
		public ImGuiMouseSource MouseSource;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEventMousePos
	{
		public float PosX;
		public float PosY;
		public ImGuiMouseSource MouseSource;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEventMouseViewport
	{
		public uint HoveredViewportID;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEventMouseWheel
	{
		public float WheelX;
		public float WheelY;
		public ImGuiMouseSource MouseSource;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputEventText
	{
		public uint Char;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputTextCallbackData
	{
		public ImGuiContext* Ctx;
		public int EventFlag;
		public int Flags;
		public void* UserData;
		public ushort EventChar;
		public ImGuiKey EventKey;
		public byte* Buf;
		public int BufTextLen;
		public int BufSize;
		public bool BufDirty;
		public int CursorPos;
		public int SelectionStart;
		public int SelectionEnd;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputTextDeactivatedState
	{
		public uint ID;
		public ImVector<byte> TextA;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiInputTextState
	{
		public ImGuiContext* Ctx;
		public uint ID;
		public int CurLenW;
		public int CurLenA;
		public ImVector<ushort> TextW;
		public ImVector<byte> TextA;
		public ImVector<byte> InitialTextA;
		public bool TextAIsValid;
		public int BufCapacityA;
		public float ScrollX;
		public STB_TexteditState Stb;
		public float CursorAnim;
		public bool CursorFollow;
		public bool SelectedAllMouseLock;
		public bool Edited;
		public int Flags;
		public bool ReloadUserBuf;
		public int ReloadSelectionStart;
		public int ReloadSelectionEnd;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiKeyData
	{
		public bool Down;
		public float DownDuration;
		public float DownDurationPrev;
		public float AnalogValue;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiKeyOwnerData
	{
		public uint OwnerCurr;
		public uint OwnerNext;
		public bool LockThisFrame;
		public bool LockUntilRelease;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiKeyRoutingData
	{
		public short NextEntryIndex;
		public ushort Mods;
		public byte RoutingCurrScore;
		public byte RoutingNextScore;
		public uint RoutingCurr;
		public uint RoutingNext;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiKeyRoutingTable
	{
		public ImGuiKeyRoutingTable_IndexArray Index;
		public ImVector<ImGuiKeyRoutingData> Entries;
		public ImVector<ImGuiKeyRoutingData> EntriesNext;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiKeyRoutingTable_IndexArray
	{
		public fixed byte Index[((int)(ImGuiKeyNamedKey.COUNT))*(2)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiLastItemData
	{
		public uint ID;
		public int InFlags;
		public int StatusFlags;
		public ImRect Rect;
		public ImRect NavRect;
		public ImRect DisplayRect;
		public ImRect ClipRect;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiListClipper
	{
		public ImGuiContext* Ctx;
		public int DisplayStart;
		public int DisplayEnd;
		public int ItemsCount;
		public float ItemsHeight;
		public float StartPosY;
		public void* TempData;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiListClipperData
	{
		public ImGuiListClipper* ListClipper;
		public float LossynessOffset;
		public int StepNo;
		public int ItemsFrozen;
		public ImVector<ImGuiListClipperRange> Ranges;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiListClipperRange
	{
		public int Min;
		public int Max;
		public bool PosToIndexConvert;
		public sbyte PosToIndexOffsetMin;
		public sbyte PosToIndexOffsetMax;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiLocEntry
	{
		public ImGuiLocKey Key;
		public byte* Text;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiMenuColumns
	{
		public uint TotalWidth;
		public uint NextTotalWidth;
		public ushort Spacing;
		public ushort OffsetIcon;
		public ushort OffsetLabel;
		public ushort OffsetShortcut;
		public ushort OffsetMark;
		public ImGuiMenuColumns_WidthsArray Widths;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiMenuColumns_WidthsArray
	{
		public fixed byte Widths[((int)(4))*(2)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiMetricsConfig
	{
		public bool ShowDebugLog;
		public bool ShowIDStackTool;
		public bool ShowWindowsRects;
		public bool ShowWindowsBeginOrder;
		public bool ShowTablesRects;
		public bool ShowDrawCmdMesh;
		public bool ShowDrawCmdBoundingBoxes;
		public bool ShowTextEncodingViewer;
		public bool ShowAtlasTintedWithTextColor;
		public bool ShowDockingNodes;
		public int ShowWindowsRectsType;
		public int ShowTablesRectsType;
		public int HighlightMonitorIdx;
		public uint HighlightViewportID;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiNavItemData
	{
		public ImGuiWindow* Window;
		public uint ID;
		public uint FocusScopeId;
		public ImRect RectRel;
		public int InFlags;
		public long SelectionUserData;
		public float DistBox;
		public float DistCenter;
		public float DistAxial;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiNavTreeNodeData
	{
		public uint ID;
		public int InFlags;
		public ImRect NavRect;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiNextItemData
	{
		public int Flags;
		public int ItemFlags;
		public long SelectionUserData;
		public float Width;
		public int Shortcut;
		public bool OpenVal;
		public int OpenCond;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiNextWindowData
	{
		public int Flags;
		public int PosCond;
		public int SizeCond;
		public int CollapsedCond;
		public int DockCond;
		public Unity.Mathematics.float2 PosVal;
		public Unity.Mathematics.float2 PosPivotVal;
		public Unity.Mathematics.float2 SizeVal;
		public Unity.Mathematics.float2 ContentSizeVal;
		public Unity.Mathematics.float2 ScrollVal;
		public int ChildFlags;
		public bool PosUndock;
		public bool CollapsedVal;
		public ImRect SizeConstraintRect;
		public delegate* unmanaged[Cdecl]<ImGuiSizeCallbackData*, void> SizeCallback;
		public void* SizeCallbackUserData;
		public float BgAlphaVal;
		public uint ViewportId;
		public uint DockId;
		public ImGuiWindowClass WindowClass;
		public Unity.Mathematics.float2 MenuBarOffsetMinVal;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiOldColumnData
	{
		public float OffsetNorm;
		public float OffsetNormBeforeResize;
		public int Flags;
		public ImRect ClipRect;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiOldColumns
	{
		public uint ID;
		public int Flags;
		public bool IsFirstFrame;
		public bool IsBeingResized;
		public int Current;
		public int Count;
		public float OffMinX;
		public float OffMaxX;
		public float LineMinY;
		public float LineMaxY;
		public float HostCursorPosY;
		public float HostCursorMaxPosX;
		public ImRect HostInitialClipRect;
		public ImRect HostBackupClipRect;
		public ImRect HostBackupParentWorkRect;
		public ImVector<ImGuiOldColumnData> Columns;
		public ImDrawListSplitter Splitter;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiOnceUponAFrame
	{
		public int RefFrame;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiPayload
	{
		public void* Data;
		public int DataSize;
		public uint SourceId;
		public uint SourceParentId;
		public int DataFrameCount;
		public ImGuiPayload_DataTypeArray DataType;
		public bool Preview;
		public bool Delivery;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiPayload_DataTypeArray
	{
		public fixed byte DataType[((int)(32+1))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiPlatformIO
	{
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Platform_CreateWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Platform_DestroyWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Platform_ShowWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, Unity.Mathematics.float2, void> Platform_SetWindowPos;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, Unity.Mathematics.float2> Platform_GetWindowPos;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, Unity.Mathematics.float2, void> Platform_SetWindowSize;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, Unity.Mathematics.float2> Platform_GetWindowSize;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Platform_SetWindowFocus;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, bool> Platform_GetWindowFocus;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, bool> Platform_GetWindowMinimized;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, byte*, void> Platform_SetWindowTitle;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, float, void> Platform_SetWindowAlpha;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Platform_UpdateWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, System.IntPtr, void> Platform_RenderWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, System.IntPtr, void> Platform_SwapBuffers;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, float> Platform_GetWindowDpiScale;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Platform_OnChangedViewport;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, ulong, System.IntPtr, ulong*, int> Platform_CreateVkSurface;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Renderer_CreateWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, void> Renderer_DestroyWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, Unity.Mathematics.float2, void> Renderer_SetWindowSize;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, System.IntPtr, void> Renderer_RenderWindow;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, System.IntPtr, void> Renderer_SwapBuffers;
		public ImVector<ImGuiPlatformMonitor> Monitors;
		public ImVector<Ptr<ImGuiViewport>> Viewports;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiPlatformImeData
	{
		public bool WantVisible;
		public Unity.Mathematics.float2 InputPos;
		public float InputLineHeight;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiPlatformMonitor
	{
		public Unity.Mathematics.float2 MainPos;
		public Unity.Mathematics.float2 MainSize;
		public Unity.Mathematics.float2 WorkPos;
		public Unity.Mathematics.float2 WorkSize;
		public float DpiScale;
		public void* PlatformHandle;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiPopupData
	{
		public uint PopupId;
		public ImGuiWindow* Window;
		public ImGuiWindow* BackupNavWindow;
		public int ParentNavLayer;
		public int OpenFrameCount;
		public uint OpenParentId;
		public Unity.Mathematics.float2 OpenPopupPos;
		public Unity.Mathematics.float2 OpenMousePos;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiPtrOrIndex
	{
		public void* Ptr;
		public int Index;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiSettingsHandler
	{
		public byte* TypeName;
		public uint TypeHash;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiSettingsHandler*, void> ClearAllFn;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiSettingsHandler*, void> ReadInitFn;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiSettingsHandler*, byte*, System.IntPtr> ReadOpenFn;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiSettingsHandler*, System.IntPtr, byte*, void> ReadLineFn;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiSettingsHandler*, void> ApplyAllFn;
		public delegate* unmanaged[Cdecl]<ImGuiContext*, ImGuiSettingsHandler*, ImGuiTextBuffer*, void> WriteAllFn;
		public void* UserData;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiShrinkWidthItem
	{
		public int Index;
		public float Width;
		public float InitialWidth;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiSizeCallbackData
	{
		public void* UserData;
		public Unity.Mathematics.float2 Pos;
		public Unity.Mathematics.float2 CurrentSize;
		public Unity.Mathematics.float2 DesiredSize;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStackLevelInfo
	{
		public uint ID;
		public sbyte QueryFrameCount;
		public bool QuerySuccess;
		public int DataType;
		public ImGuiStackLevelInfo_DescArray Desc;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStackLevelInfo_DescArray
	{
		public fixed byte Desc[((int)(57))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStackSizes
	{
		public short SizeOfIDStack;
		public short SizeOfColorStack;
		public short SizeOfStyleVarStack;
		public short SizeOfFontStack;
		public short SizeOfFocusScopeStack;
		public short SizeOfGroupStack;
		public short SizeOfItemFlagsStack;
		public short SizeOfBeginPopupStack;
		public short SizeOfDisabledStack;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStorage
	{
		public ImVector<ImGuiStoragePair> Data;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStoragePair
	{
		public uint key;
		public ImGuiStoragePairUnion union;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStyle
	{
		public float Alpha;
		public float DisabledAlpha;
		public Unity.Mathematics.float2 WindowPadding;
		public float WindowRounding;
		public float WindowBorderSize;
		public Unity.Mathematics.float2 WindowMinSize;
		public Unity.Mathematics.float2 WindowTitleAlign;
		public int WindowMenuButtonPosition;
		public float ChildRounding;
		public float ChildBorderSize;
		public float PopupRounding;
		public float PopupBorderSize;
		public Unity.Mathematics.float2 FramePadding;
		public float FrameRounding;
		public float FrameBorderSize;
		public Unity.Mathematics.float2 ItemSpacing;
		public Unity.Mathematics.float2 ItemInnerSpacing;
		public Unity.Mathematics.float2 CellPadding;
		public Unity.Mathematics.float2 TouchExtraPadding;
		public float IndentSpacing;
		public float ColumnsMinSpacing;
		public float ScrollbarSize;
		public float ScrollbarRounding;
		public float GrabMinSize;
		public float GrabRounding;
		public float LogSliderDeadzone;
		public float TabRounding;
		public float TabBorderSize;
		public float TabMinWidthForCloseButton;
		public float TabBarBorderSize;
		public float TableAngledHeadersAngle;
		public int ColorButtonPosition;
		public Unity.Mathematics.float2 ButtonTextAlign;
		public Unity.Mathematics.float2 SelectableTextAlign;
		public float SeparatorTextBorderSize;
		public Unity.Mathematics.float2 SeparatorTextAlign;
		public Unity.Mathematics.float2 SeparatorTextPadding;
		public Unity.Mathematics.float2 DisplayWindowPadding;
		public Unity.Mathematics.float2 DisplaySafeAreaPadding;
		public float DockingSeparatorSize;
		public float MouseCursorScale;
		public bool AntiAliasedLines;
		public bool AntiAliasedLinesUseTex;
		public bool AntiAliasedFill;
		public float CurveTessellationTol;
		public float CircleTessellationMaxError;
		public ImGuiStyle_ColorsArray Colors;
		public float HoverStationaryDelay;
		public float HoverDelayShort;
		public float HoverDelayNormal;
		public int HoverFlagsForTooltipMouse;
		public int HoverFlagsForTooltipNav;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStyle_ColorsArray
	{
		public fixed byte Colors[((int)(ImGuiCol.COUNT))*(16)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStyleMod
	{
		public int VarIdx;
		public ImGuiStyleModUnion union;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTabBar
	{
		public ImVector<ImGuiTabItem> Tabs;
		public int Flags;
		public uint ID;
		public uint SelectedTabId;
		public uint NextSelectedTabId;
		public uint VisibleTabId;
		public int CurrFrameVisible;
		public int PrevFrameVisible;
		public ImRect BarRect;
		public float CurrTabsContentsHeight;
		public float PrevTabsContentsHeight;
		public float WidthAllTabs;
		public float WidthAllTabsIdeal;
		public float ScrollingAnim;
		public float ScrollingTarget;
		public float ScrollingTargetDistToVisibility;
		public float ScrollingSpeed;
		public float ScrollingRectMinX;
		public float ScrollingRectMaxX;
		public float SeparatorMinX;
		public float SeparatorMaxX;
		public uint ReorderRequestTabId;
		public short ReorderRequestOffset;
		public sbyte BeginCount;
		public bool WantLayout;
		public bool VisibleTabWasSubmitted;
		public bool TabsAddedNew;
		public short TabsActiveCount;
		public short LastTabItemIdx;
		public float ItemSpacingY;
		public Unity.Mathematics.float2 FramePadding;
		public Unity.Mathematics.float2 BackupCursorPos;
		public ImGuiTextBuffer TabsNames;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTabItem
	{
		public uint ID;
		public int Flags;
		public ImGuiWindow* Window;
		public int LastFrameVisible;
		public int LastFrameSelected;
		public float Offset;
		public float Width;
		public float ContentWidth;
		public float RequestedWidth;
		public int NameOffset;
		public short BeginOrder;
		public short IndexDuringLayout;
		public bool WantClose;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTable
	{
		public uint ID;
		public int Flags;
		public void* RawData;
		public ImGuiTableTempData* TempData;
		public ImSpan<ImGuiTableColumn> Columns;
		public ImSpan<short> DisplayOrderToIndex;
		public ImSpan<ImGuiTableCellData> RowCellData;
		public uint* EnabledMaskByDisplayOrder;
		public uint* EnabledMaskByIndex;
		public uint* VisibleMaskByIndex;
		public int SettingsLoadedFlags;
		public int SettingsOffset;
		public int LastFrameActive;
		public int ColumnsCount;
		public int CurrentRow;
		public int CurrentColumn;
		public short InstanceCurrent;
		public short InstanceInteracted;
		public float RowPosY1;
		public float RowPosY2;
		public float RowMinHeight;
		public float RowCellPaddingY;
		public float RowTextBaseline;
		public float RowIndentOffsetX;
		public int RowFlags;
		public int LastRowFlags;
		public int RowBgColorCounter;
		public ImGuiTable_RowBgColorArray RowBgColor;
		public uint BorderColorStrong;
		public uint BorderColorLight;
		public float BorderX1;
		public float BorderX2;
		public float HostIndentX;
		public float MinColumnWidth;
		public float OuterPaddingX;
		public float CellPaddingX;
		public float CellSpacingX1;
		public float CellSpacingX2;
		public float InnerWidth;
		public float ColumnsGivenWidth;
		public float ColumnsAutoFitWidth;
		public float ColumnsStretchSumWeights;
		public float ResizedColumnNextWidth;
		public float ResizeLockMinContentsX2;
		public float RefScale;
		public float AngledHeadersHeight;
		public float AngledHeadersSlope;
		public ImRect OuterRect;
		public ImRect InnerRect;
		public ImRect WorkRect;
		public ImRect InnerClipRect;
		public ImRect BgClipRect;
		public ImRect Bg0ClipRectForDrawCmd;
		public ImRect Bg2ClipRectForDrawCmd;
		public ImRect HostClipRect;
		public ImRect HostBackupInnerClipRect;
		public ImGuiWindow* OuterWindow;
		public ImGuiWindow* InnerWindow;
		public ImGuiTextBuffer ColumnsNames;
		public ImDrawListSplitter* DrawSplitter;
		public ImGuiTableInstanceData InstanceDataFirst;
		public ImVector<ImGuiTableInstanceData> InstanceDataExtra;
		public ImGuiTableColumnSortSpecs SortSpecsSingle;
		public ImVector<ImGuiTableColumnSortSpecs> SortSpecsMulti;
		public ImGuiTableSortSpecs SortSpecs;
		public short SortSpecsCount;
		public short ColumnsEnabledCount;
		public short ColumnsEnabledFixedCount;
		public short DeclColumnsCount;
		public short AngledHeadersCount;
		public short HoveredColumnBody;
		public short HoveredColumnBorder;
		public short HighlightColumnHeader;
		public short AutoFitSingleColumn;
		public short ResizedColumn;
		public short LastResizedColumn;
		public short HeldHeaderColumn;
		public short ReorderColumn;
		public short ReorderColumnDir;
		public short LeftMostEnabledColumn;
		public short RightMostEnabledColumn;
		public short LeftMostStretchedColumn;
		public short RightMostStretchedColumn;
		public short ContextPopupColumn;
		public short FreezeRowsRequest;
		public short FreezeRowsCount;
		public short FreezeColumnsRequest;
		public short FreezeColumnsCount;
		public short RowCellDataCurrent;
		public ushort DummyDrawChannel;
		public ushort Bg2DrawChannelCurrent;
		public ushort Bg2DrawChannelUnfrozen;
		public bool IsLayoutLocked;
		public bool IsInsideRow;
		public bool IsInitializing;
		public bool IsSortSpecsDirty;
		public bool IsUsingHeaders;
		public bool IsContextPopupOpen;
		public bool DisableDefaultContextMenu;
		public bool IsSettingsRequestLoad;
		public bool IsSettingsDirty;
		public bool IsDefaultDisplayOrder;
		public bool IsResetAllRequest;
		public bool IsResetDisplayOrderRequest;
		public bool IsUnfrozenRows;
		public bool IsDefaultSizingPolicy;
		public bool IsActiveIdAliveBeforeTable;
		public bool IsActiveIdInTable;
		public bool HasScrollbarYCurr;
		public bool HasScrollbarYPrev;
		public bool MemoryCompacted;
		public bool HostSkipItems;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTable_RowBgColorArray
	{
		public fixed byte RowBgColor[((int)(2))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableCellData
	{
		public uint BgColor;
		public short Column;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableColumn
	{
		public int Flags;
		public float WidthGiven;
		public float MinX;
		public float MaxX;
		public float WidthRequest;
		public float WidthAuto;
		public float StretchWeight;
		public float InitStretchWeightOrWidth;
		public ImRect ClipRect;
		public uint UserID;
		public float WorkMinX;
		public float WorkMaxX;
		public float ItemWidth;
		public float ContentMaxXFrozen;
		public float ContentMaxXUnfrozen;
		public float ContentMaxXHeadersUsed;
		public float ContentMaxXHeadersIdeal;
		public short NameOffset;
		public short DisplayOrder;
		public short IndexWithinEnabledSet;
		public short PrevEnabledColumn;
		public short NextEnabledColumn;
		public short SortOrder;
		public ushort DrawChannelCurrent;
		public ushort DrawChannelFrozen;
		public ushort DrawChannelUnfrozen;
		public bool IsEnabled;
		public bool IsUserEnabled;
		public bool IsUserEnabledNextFrame;
		public bool IsVisibleX;
		public bool IsVisibleY;
		public bool IsRequestOutput;
		public bool IsSkipItems;
		public bool IsPreserveWidthAuto;
		public sbyte NavLayerCurrent;
		public byte AutoFitQueue;
		public byte CannotSkipItemsQueue;
		public byte SortDirection;
		public byte SortDirectionsAvailCount;
		public byte SortDirectionsAvailMask;
		public byte SortDirectionsAvailList;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableColumnSettings
	{
		public float WidthOrWeight;
		public uint UserID;
		public short Index;
		public short DisplayOrder;
		public short SortOrder;
		public byte SortDirection;
		public byte IsEnabled;
		public byte IsStretch;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableColumnSortSpecs
	{
		public uint ColumnUserID;
		public short ColumnIndex;
		public short SortOrder;
		public int SortDirection;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableInstanceData
	{
		public uint TableInstanceID;
		public float LastOuterHeight;
		public float LastTopHeadersRowHeight;
		public float LastFrozenHeight;
		public int HoveredRowLast;
		public int HoveredRowNext;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableSettings
	{
		public uint ID;
		public int SaveFlags;
		public float RefScale;
		public short ColumnsCount;
		public short ColumnsCountMax;
		public bool WantApply;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableSortSpecs
	{
		public ImGuiTableColumnSortSpecs* Specs;
		public int SpecsCount;
		public bool SpecsDirty;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableTempData
	{
		public int TableIndex;
		public float LastTimeActive;
		public float AngledHeadersExtraWidth;
		public Unity.Mathematics.float2 UserOuterSize;
		public ImDrawListSplitter DrawSplitter;
		public ImRect HostBackupWorkRect;
		public ImRect HostBackupParentWorkRect;
		public Unity.Mathematics.float2 HostBackupPrevLineSize;
		public Unity.Mathematics.float2 HostBackupCurrLineSize;
		public Unity.Mathematics.float2 HostBackupCursorMaxPos;
		public float HostBackupColumnsOffset;
		public float HostBackupItemWidth;
		public int HostBackupItemWidthStackSize;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTextBuffer
	{
		public ImVector<byte> Buf;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTextFilter
	{
		public ImGuiTextFilter_InputBufArray InputBuf;
		public ImVector<ImGuiTextRange> Filters;
		public int CountGrep;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTextFilter_InputBufArray
	{
		public fixed byte InputBuf[((int)(256))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTextIndex
	{
		public ImVector<int> LineOffsets;
		public int EndOffset;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTextRange
	{
		public byte* b;
		public byte* e;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTypingSelectRequest
	{
		public int Flags;
		public int SearchBufferLen;
		public byte* SearchBuffer;
		public bool SelectRequest;
		public bool SingleCharMode;
		public sbyte SingleCharSize;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTypingSelectState
	{
		public ImGuiTypingSelectRequest Request;
		public ImGuiTypingSelectState_SearchBufferArray SearchBuffer;
		public uint FocusScope;
		public int LastRequestFrame;
		public float LastRequestTime;
		public bool SingleCharModeLock;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTypingSelectState_SearchBufferArray
	{
		public fixed byte SearchBuffer[((int)(64))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiViewport
	{
		public uint ID;
		public int Flags;
		public Unity.Mathematics.float2 Pos;
		public Unity.Mathematics.float2 Size;
		public Unity.Mathematics.float2 WorkPos;
		public Unity.Mathematics.float2 WorkSize;
		public float DpiScale;
		public uint ParentViewportId;
		public ImDrawData* DrawData;
		public void* RendererUserData;
		public void* PlatformUserData;
		public void* PlatformHandle;
		public void* PlatformHandleRaw;
		public bool PlatformWindowCreated;
		public bool PlatformRequestMove;
		public bool PlatformRequestResize;
		public bool PlatformRequestClose;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiViewportP
	{
		public ImGuiViewport _ImGuiViewport;
		public ImGuiWindow* Window;
		public int Idx;
		public int LastFrameActive;
		public int LastFocusedStampCount;
		public uint LastNameHash;
		public Unity.Mathematics.float2 LastPos;
		public float Alpha;
		public float LastAlpha;
		public bool LastFocusedHadNavWindow;
		public short PlatformMonitor;
		public ImGuiViewportP_BgFgDrawListsLastFrameArray BgFgDrawListsLastFrame;
		public ImGuiViewportP_BgFgDrawListsArray* BgFgDrawLists;
		public ImDrawData DrawDataP;
		public ImDrawDataBuilder DrawDataBuilder;
		public Unity.Mathematics.float2 LastPlatformPos;
		public Unity.Mathematics.float2 LastPlatformSize;
		public Unity.Mathematics.float2 LastRendererSize;
		public Unity.Mathematics.float2 WorkOffsetMin;
		public Unity.Mathematics.float2 WorkOffsetMax;
		public Unity.Mathematics.float2 BuildWorkOffsetMin;
		public Unity.Mathematics.float2 BuildWorkOffsetMax;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiViewportP_BgFgDrawListsLastFrameArray
	{
		public fixed byte BgFgDrawListsLastFrame[((int)(2))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiViewportP_BgFgDrawListsArray
	{
		public fixed byte BgFgDrawLists[((int)(2))*(ImGuiConstants.PtrSize)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindow
	{
		public ImGuiContext* Ctx;
		public byte* Name;
		public uint ID;
		public int Flags;
		public int FlagsPreviousFrame;
		public int ChildFlags;
		public ImGuiWindowClass WindowClass;
		public ImGuiViewportP* Viewport;
		public uint ViewportId;
		public Unity.Mathematics.float2 ViewportPos;
		public int ViewportAllowPlatformMonitorExtend;
		public Unity.Mathematics.float2 Pos;
		public Unity.Mathematics.float2 Size;
		public Unity.Mathematics.float2 SizeFull;
		public Unity.Mathematics.float2 ContentSize;
		public Unity.Mathematics.float2 ContentSizeIdeal;
		public Unity.Mathematics.float2 ContentSizeExplicit;
		public Unity.Mathematics.float2 WindowPadding;
		public float WindowRounding;
		public float WindowBorderSize;
		public float DecoOuterSizeX1;
		public float DecoOuterSizeY1;
		public float DecoOuterSizeX2;
		public float DecoOuterSizeY2;
		public float DecoInnerSizeX1;
		public float DecoInnerSizeY1;
		public int NameBufLen;
		public uint MoveId;
		public uint TabId;
		public uint ChildId;
		public Unity.Mathematics.float2 Scroll;
		public Unity.Mathematics.float2 ScrollMax;
		public Unity.Mathematics.float2 ScrollTarget;
		public Unity.Mathematics.float2 ScrollTargetCenterRatio;
		public Unity.Mathematics.float2 ScrollTargetEdgeSnapDist;
		public Unity.Mathematics.float2 ScrollbarSizes;
		public bool ScrollbarX;
		public bool ScrollbarY;
		public bool ViewportOwned;
		public bool Active;
		public bool WasActive;
		public bool WriteAccessed;
		public bool Collapsed;
		public bool WantCollapseToggle;
		public bool SkipItems;
		public bool Appearing;
		public bool Hidden;
		public bool IsFallbackWindow;
		public bool IsExplicitChild;
		public bool HasCloseButton;
		public sbyte ResizeBorderHovered;
		public sbyte ResizeBorderHeld;
		public short BeginCount;
		public short BeginCountPreviousFrame;
		public short BeginOrderWithinParent;
		public short BeginOrderWithinContext;
		public short FocusOrder;
		public uint PopupId;
		public sbyte AutoFitFramesX;
		public sbyte AutoFitFramesY;
		public bool AutoFitOnlyGrows;
		public int AutoPosLastDirection;
		public sbyte HiddenFramesCanSkipItems;
		public sbyte HiddenFramesCannotSkipItems;
		public sbyte HiddenFramesForRenderOnly;
		public sbyte DisableInputsFrames;
		public int SetWindowPosAllowFlags;
		public int SetWindowSizeAllowFlags;
		public int SetWindowCollapsedAllowFlags;
		public int SetWindowDockAllowFlags;
		public Unity.Mathematics.float2 SetWindowPosVal;
		public Unity.Mathematics.float2 SetWindowPosPivot;
		public ImVector<uint> IDStack;
		public ImGuiWindowTempData DC;
		public ImRect OuterRectClipped;
		public ImRect InnerRect;
		public ImRect InnerClipRect;
		public ImRect WorkRect;
		public ImRect ParentWorkRect;
		public ImRect ClipRect;
		public ImRect ContentRegionRect;
		public Unity.Mathematics.int2 HitTestHoleSize;
		public Unity.Mathematics.int2 HitTestHoleOffset;
		public int LastFrameActive;
		public int LastFrameJustFocused;
		public float LastTimeActive;
		public float ItemWidthDefault;
		public ImGuiStorage StateStorage;
		public ImVector<ImGuiOldColumns> ColumnsStorage;
		public float FontWindowScale;
		public float FontDpiScale;
		public int SettingsOffset;
		public ImDrawList* DrawList;
		public ImDrawList DrawListInst;
		public ImGuiWindow* ParentWindow;
		public ImGuiWindow* ParentWindowInBeginStack;
		public ImGuiWindow* RootWindow;
		public ImGuiWindow* RootWindowPopupTree;
		public ImGuiWindow* RootWindowDockTree;
		public ImGuiWindow* RootWindowForTitleBarHighlight;
		public ImGuiWindow* RootWindowForNav;
		public ImGuiWindow* ParentWindowForFocusRoute;
		public ImGuiWindow* NavLastChildNavWindow;
		public ImGuiWindow_NavLastIdsArray NavLastIds;
		public ImGuiWindow_NavRectRelArray NavRectRel;
		public ImGuiWindow_NavPreferredScoringPosRelArray NavPreferredScoringPosRel;
		public uint NavRootFocusScopeId;
		public int MemoryDrawListIdxCapacity;
		public int MemoryDrawListVtxCapacity;
		public bool MemoryCompacted;
		public bool DockIsActive;
		public bool DockNodeIsVisible;
		public bool DockTabIsVisible;
		public bool DockTabWantClose;
		public short DockOrder;
		public ImGuiWindowDockStyle DockStyle;
		public ImGuiDockNode* DockNode;
		public ImGuiDockNode* DockNodeAsHost;
		public uint DockId;
		public int DockTabItemStatusFlags;
		public ImRect DockTabItemRect;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindow_NavLastIdsArray
	{
		public fixed byte NavLastIds[((int)(ImGuiNavLayer.COUNT))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindow_NavRectRelArray
	{
		public fixed byte NavRectRel[((int)(ImGuiNavLayer.COUNT))*(0+8+8)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindow_NavPreferredScoringPosRelArray
	{
		public fixed byte NavPreferredScoringPosRel[((int)(ImGuiNavLayer.COUNT))*(8)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindowClass
	{
		public uint ClassId;
		public uint ParentViewportId;
		public uint FocusRouteParentWindowId;
		public int ViewportFlagsOverrideSet;
		public int ViewportFlagsOverrideClear;
		public int TabItemFlagsOverrideSet;
		public int DockNodeFlagsOverrideSet;
		public bool DockingAlwaysTabBar;
		public bool DockingAllowUnclassed;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindowDockStyle
	{
		public ImGuiWindowDockStyle_ColorsArray Colors;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindowDockStyle_ColorsArray
	{
		public fixed byte Colors[((int)(ImGuiWindowDockStyleCol.COUNT))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindowSettings
	{
		public uint ID;
		public Unity.Mathematics.int2 Pos;
		public Unity.Mathematics.int2 Size;
		public Unity.Mathematics.int2 ViewportPos;
		public uint ViewportId;
		public uint DockId;
		public uint ClassId;
		public short DockOrder;
		public bool Collapsed;
		public bool IsChild;
		public bool WantApply;
		public bool WantDelete;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindowStackData
	{
		public ImGuiWindow* Window;
		public ImGuiLastItemData ParentLastItemDataBackup;
		public ImGuiStackSizes StackSizesOnBegin;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindowTempData
	{
		public Unity.Mathematics.float2 CursorPos;
		public Unity.Mathematics.float2 CursorPosPrevLine;
		public Unity.Mathematics.float2 CursorStartPos;
		public Unity.Mathematics.float2 CursorMaxPos;
		public Unity.Mathematics.float2 IdealMaxPos;
		public Unity.Mathematics.float2 CurrLineSize;
		public Unity.Mathematics.float2 PrevLineSize;
		public float CurrLineTextBaseOffset;
		public float PrevLineTextBaseOffset;
		public bool IsSameLine;
		public bool IsSetPos;
		public float Indent;
		public float ColumnsOffset;
		public float GroupOffset;
		public Unity.Mathematics.float2 CursorStartPosLossyness;
		public ImGuiNavLayer NavLayerCurrent;
		public short NavLayersActiveMask;
		public short NavLayersActiveMaskNext;
		public bool NavIsScrollPushableX;
		public bool NavHideHighlightOneFrame;
		public bool NavWindowHasScrollY;
		public bool MenuBarAppending;
		public Unity.Mathematics.float2 MenuBarOffset;
		public ImGuiMenuColumns MenuColumns;
		public int TreeDepth;
		public uint TreeJumpToParentOnPopMask;
		public ImVector<Ptr<ImGuiWindow>> ChildWindows;
		public ImGuiStorage* StateStorage;
		public ImGuiOldColumns* CurrentColumns;
		public int CurrentTableIdx;
		public int LayoutType;
		public int ParentLayoutType;
		public uint ModalDimBgColor;
		public float ItemWidth;
		public float TextWrapPos;
		public ImVector<float> ItemWidthStack;
		public ImVector<float> TextWrapPosStack;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImRect
	{
		public Unity.Mathematics.float2 Min;
		public Unity.Mathematics.float2 Max;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct STB_TexteditState
	{
		public int cursor;
		public int select_start;
		public int select_end;
		public byte insert_mode;
		public int row_count_per_page;
		public byte cursor_at_end_of_line;
		public byte initialized;
		public byte has_preferred_x;
		public byte single_line;
		public byte padding1;
		public byte padding2;
		public byte padding3;
		public float preferred_x;
		public StbUndoState undostate;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct StbTexteditRow
	{
		public float x0;
		public float x1;
		public float baseline_y_delta;
		public float ymin;
		public float ymax;
		public int num_chars;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct StbUndoRecord
	{
		public int where;
		public int insert_length;
		public int delete_length;
		public int char_storage;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct StbUndoState
	{
		public StbUndoState_undo_recArray undo_rec;
		public StbUndoState_undo_charArray undo_char;
		public short undo_point;
		public short redo_point;
		public int undo_char_point;
		public int redo_char_point;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct StbUndoState_undo_recArray
	{
		public fixed byte undo_rec[((int)(99))*(0+4+4+4+4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct StbUndoState_undo_charArray
	{
		public fixed byte undo_char[((int)(999))*(2)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImBitArray
	{
		public ImBitArray_StorageArray Storage;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImBitArray_StorageArray
	{
		public fixed byte Storage[((int)(ImGuiKeyNamedKey.COUNT))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImChunkStream<T> where T : unmanaged
	{
		public ImVector<byte> Buf;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImPool<T> where T : unmanaged
	{
		public ImVector<T> Buf;
		public ImGuiStorage Map;
		public int FreeIdx;
		public int AliveCount;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImSpan<T> where T : unmanaged
	{
		public T* Data;
		public T* DataEnd;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImSpanAllocator
	{
		public byte* BasePtr;
		public int CurrOff;
		public int CurrIdx;
		public ImSpanAllocator_OffsetsArray Offsets;
		public ImSpanAllocator_SizesArray Sizes;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImSpanAllocator_OffsetsArray
	{
		public fixed byte Offsets[((int)(1))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImSpanAllocator_SizesArray
	{
		public fixed byte Sizes[((int)(1))*(4)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe partial struct ImVector<T> where T : unmanaged
	{
		public int Size;
		public int Capacity;
		public T* Data;
	}

}
