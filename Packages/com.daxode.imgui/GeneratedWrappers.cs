using System;
namespace com.daxode.imgui.generated
{
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

}
