using System;
using System.Runtime.InteropServices;
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

	[Flags]
	public enum ImGuiSelectableFlags
	{
		None = 0,
		DontClosePopups = 1 << 0,
		SpanAllColumns = 1 << 1,
		AllowDoubleClick = 1 << 2,
		Disabled = 1 << 3,
		AllowOverlap = 1 << 4,
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
	unsafe struct ImDrawChannel
	{
		public ImVector<ImDrawCmd> _CmdBuffer;
		public ImVector<ushort> _IdxBuffer;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe partial struct ImDrawCmd
	{
		public Unity.Mathematics.float4 ClipRect;
		public UnityObjRef<UnityEngine.Texture2D> TextureId;
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
		public UnityObjRef<UnityEngine.Texture2D> TextureId;
		public uint VtxOffset;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawData
	{
		public byte Valid;
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
	unsafe struct ImDrawList
	{
		public ImVector<ImDrawCmd> CmdBuffer;
		public ImVector<ushort> IdxBuffer;
		public ImVector<ImDrawVert> VtxBuffer;
		public ImDrawListFlags Flags;
		public uint _VtxCurrentIdx;
		public ImDrawListSharedData* _Data;
		public byte* _OwnerName;
		public ImDrawVert* _VtxWritePtr;
		public ushort* _IdxWritePtr;
		public ImVector<Unity.Mathematics.float4> _ClipRectStack;
		public ImVector<UnityObjRef<UnityEngine.Texture2D>> _TextureIdStack;
		public ImVector<Unity.Mathematics.float2> _Path;
		public ImDrawCmdHeader _CmdHeader;
		public ImDrawListSplitter _Splitter;
		public float _FringeScale;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawListSplitter
	{
		public int _Current;
		public int _Count;
		public ImVector<ImDrawChannel> _Channels;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImDrawVert
	{
		public Unity.Mathematics.float2 pos;
		public uint col;
		public Unity.Mathematics.float2 uv;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFont
	{
		public ImVector<float> IndexAdvanceX;
		public float FallbackAdvanceX;
		public float FontSize;
		public ImVector<uint> IndexLookup;
		public ImVector<ImFontGlyph> Glyphs;
		public ImFontGlyph* FallbackGlyph;
		public ImFontAtlas* ContainerAtlas;
		public ImFontConfig* ConfigData;
		public short ConfigDataCount;
		public uint FallbackChar;
		public uint EllipsisChar;
		public short EllipsisCharCount;
		public float EllipsisWidth;
		public float EllipsisCharStep;
		public byte DirtyLookupTables;
		public float Scale;
		public float Ascent;
		public float Descent;
		public int MetricsTotalSurface;
		public ImFont_Used4kPagesMapArray Used4kPagesMap;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImFont_Used4kPagesMapArray
	{
		public fixed byte Used4kPagesMap[((int)((0x10FFFF+1)/4096/8))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe partial struct ImFontAtlas
	{
		public ImFontAtlasFlags Flags;
		public UnityObjRef<UnityEngine.Texture2D> TexID;
		public int TexDesiredWidth;
		public int TexGlyphPadding;
		public byte Locked;
		public void* UserData;
		public byte TexReady;
		public byte TexPixelsUseColors;
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
	unsafe struct ImFontConfig
	{
		public void* FontData;
		public int FontDataSize;
		public byte FontDataOwnedByAtlas;
		public int FontNo;
		public float SizePixels;
		public int OversampleH;
		public int OversampleV;
		public byte PixelSnapH;
		public Unity.Mathematics.float2 GlyphExtraSpacing;
		public Unity.Mathematics.float2 GlyphOffset;
		public uint* GlyphRanges;
		public float GlyphMinAdvanceX;
		public float GlyphMaxAdvanceX;
		public byte MergeMode;
		public uint FontBuilderFlags;
		public float RasterizerMultiply;
		public float RasterizerDensity;
		public uint EllipsisChar;
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
		public byte FontAllowUserScaling;
		public ImFont* FontDefault;
		public Unity.Mathematics.float2 DisplayFramebufferScale;
		public byte ConfigDockingNoSplit;
		public byte ConfigDockingWithShift;
		public byte ConfigDockingAlwaysTabBar;
		public byte ConfigDockingTransparentPayload;
		public byte ConfigViewportsNoAutoMerge;
		public byte ConfigViewportsNoTaskBarIcon;
		public byte ConfigViewportsNoDecoration;
		public byte ConfigViewportsNoDefaultParent;
		public byte MouseDrawCursor;
		public byte ConfigMacOSXBehaviors;
		public byte ConfigInputTrickleEventQueue;
		public byte ConfigInputTextCursorBlink;
		public byte ConfigInputTextEnterKeepActive;
		public byte ConfigDragClickToInputText;
		public byte ConfigWindowsResizeFromEdges;
		public byte ConfigWindowsMoveFromTitleBarOnly;
		public float ConfigMemoryCompactTimer;
		public float MouseDoubleClickTime;
		public float MouseDoubleClickMaxDist;
		public float MouseDragThreshold;
		public float KeyRepeatDelay;
		public float KeyRepeatRate;
		public byte ConfigDebugIsDebuggerPresent;
		public byte ConfigDebugBeginReturnValueOnce;
		public byte ConfigDebugBeginReturnValueLoop;
		public byte ConfigDebugIgnoreFocusLoss;
		public byte ConfigDebugIniSettings;
		public byte* BackendPlatformName;
		public byte* BackendRendererName;
		public void* BackendPlatformUserData;
		public void* BackendRendererUserData;
		public void* BackendLanguageUserData;
		public delegate* unmanaged[Cdecl]<System.IntPtr, char*> GetClipboardTextFn;
		public delegate* unmanaged[Cdecl]<System.IntPtr, byte*, void> SetClipboardTextFn;
		public void* ClipboardUserData;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, ImGuiPlatformImeData*, void> SetPlatformImeDataFn;
		public uint PlatformLocaleDecimalPoint;
		public byte WantCaptureMouse;
		public byte WantCaptureKeyboard;
		public byte WantTextInput;
		public byte WantSetMousePos;
		public byte WantSaveIniSettings;
		public byte NavActive;
		public byte NavVisible;
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
		public byte KeyCtrl;
		public byte KeyShift;
		public byte KeyAlt;
		public byte KeySuper;
		public int KeyMods;
		public ImGuiIO_KeysDataArray KeysData;
		public byte WantCaptureMouseUnlessPopupClose;
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
		public byte MouseWheelRequestAxisSwap;
		public ImGuiIO_MouseDownDurationArray MouseDownDuration;
		public ImGuiIO_MouseDownDurationPrevArray MouseDownDurationPrev;
		public ImGuiIO_MouseDragMaxDistanceAbsArray MouseDragMaxDistanceAbs;
		public ImGuiIO_MouseDragMaxDistanceSqrArray MouseDragMaxDistanceSqr;
		public float PenPressure;
		public byte AppFocusLost;
		public byte AppAcceptingEvents;
		public sbyte BackendUsingLegacyKeyArrays;
		public byte BackendUsingLegacyNavInputArray;
		public ushort InputQueueSurrogate;
		public ImVector<uint> InputQueueCharacters;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_MouseDownArray
	{
		public fixed byte MouseDown[((int)(5))*(1)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiIO_KeysDataArray
	{
		public fixed byte KeysData[((int)(ImGuiKeyKeysData.SIZE))*(16)];
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
	unsafe struct ImGuiInputTextCallbackData
	{
		public ImGuiContext* Ctx;
		public ImGuiInputTextFlags EventFlag;
		public ImGuiInputTextFlags Flags;
		public void* UserData;
		public uint EventChar;
		public ImGuiKey EventKey;
		public byte* Buf;
		public int BufTextLen;
		public int BufSize;
		public byte BufDirty;
		public int CursorPos;
		public int SelectionStart;
		public int SelectionEnd;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiKeyData
	{
		public byte Down;
		public float DownDuration;
		public float DownDurationPrev;
		public float AnalogValue;
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
		public byte Preview;
		public byte Delivery;
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
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, byte> Platform_GetWindowFocus;
		public delegate* unmanaged[Cdecl]<ImGuiViewport*, byte> Platform_GetWindowMinimized;
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
		public byte WantVisible;
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
	unsafe struct ImGuiSizeCallbackData
	{
		public void* UserData;
		public Unity.Mathematics.float2 Pos;
		public Unity.Mathematics.float2 CurrentSize;
		public Unity.Mathematics.float2 DesiredSize;
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
		public ImGuiDir WindowMenuButtonPosition;
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
		public ImGuiDir ColorButtonPosition;
		public Unity.Mathematics.float2 ButtonTextAlign;
		public Unity.Mathematics.float2 SelectableTextAlign;
		public float SeparatorTextBorderSize;
		public Unity.Mathematics.float2 SeparatorTextAlign;
		public Unity.Mathematics.float2 SeparatorTextPadding;
		public Unity.Mathematics.float2 DisplayWindowPadding;
		public Unity.Mathematics.float2 DisplaySafeAreaPadding;
		public float DockingSeparatorSize;
		public float MouseCursorScale;
		public byte AntiAliasedLines;
		public byte AntiAliasedLinesUseTex;
		public byte AntiAliasedFill;
		public float CurveTessellationTol;
		public float CircleTessellationMaxError;
		public ImGuiStyle_ColorsArray Colors;
		public float HoverStationaryDelay;
		public float HoverDelayShort;
		public float HoverDelayNormal;
		public ImGuiHoveredFlags HoverFlagsForTooltipMouse;
		public ImGuiHoveredFlags HoverFlagsForTooltipNav;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiStyle_ColorsArray
	{
		public fixed byte Colors[((int)(ImGuiCol.COUNT))*(16)];
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableColumnSortSpecs
	{
		public uint ColumnUserID;
		public short ColumnIndex;
		public short SortOrder;
		public ImGuiSortDirection SortDirection;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiTableSortSpecs
	{
		public ImGuiTableColumnSortSpecs* Specs;
		public int SpecsCount;
		public byte SpecsDirty;
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
	unsafe struct ImGuiTextRange
	{
		public byte* b;
		public byte* e;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiViewport
	{
		public uint ID;
		public ImGuiViewportFlags Flags;
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
		public byte PlatformWindowCreated;
		public byte PlatformRequestMove;
		public byte PlatformRequestResize;
		public byte PlatformRequestClose;
	}

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ImGuiWindowClass
	{
		public uint ClassId;
		public uint ParentViewportId;
		public uint FocusRouteParentWindowId;
		public ImGuiViewportFlags ViewportFlagsOverrideSet;
		public ImGuiViewportFlags ViewportFlagsOverrideClear;
		public ImGuiTabItemFlags TabItemFlagsOverrideSet;
		public ImGuiDockNodeFlags DockNodeFlagsOverrideSet;
		public byte DockingAlwaysTabBar;
		public byte DockingAllowUnclassed;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct ImVector<T> where T : unmanaged
	{
		public int Size;
		public int Capacity;
		public T* Data;
	}

}
