StreetScroller

This is a MonoTouch port of the Apple StreetScroller iOS sample:

https://developer.apple.com/library/ios/#samplecode/StreetScroller/Introduction/Intro.html

The version ported was the 1.1, 2011-08-10

The sample was part of the following session of the Apple WWDC 2011

Session 104 - "Advanced Scrollview Techniques" by Josh Shaffer and Eliza Block

You can find the session video here if you are a registered Apple developer:

https://developer.apple.com/videos/wwdc/2011/#advanced-scrollview-techniques

The slides of the presentation:

http://adcdownload.apple.com//wwdc_2011/adc_on_itunes__wwdc11_sessions__pdf/104_advanced_scroll_view_techniques.pdf

===========================================================================
ABSTRACT

Demonstrates how a UIScrollView subclass can scroll infinitely in the horizontal direction.

================================================================================
BUILD REQUIREMENTS:

Xcode 4.0 or later, iOS 4.3 SDK or later
MonoTouch 5.2 SDK or later
MonoDevelop 2.8.6.4 or later

================================================================================
RUNTIME REQUIREMENTS:

iOS 4.3 or later

===========================================================================
PACKAGING LIST

AppDelegate
This is the App Delegate that sets up the initial view controller.

StreetScrollerViewController
This view controller contains an instance of InfiniteScrollView.

InfiniteScrollView
This view tiles UILabel instances to give the effect of infinite scrolling side to side.

================================================================================
CHANGES FROM PREVIOUS VERSIONS:

Version 1.0
- First version.

===========================================================================

