# Welcome Release History 

```Minimum configuration DNN 6.0.3+ / DNN 7+ / .NET 3.5```

## 02.02.00

20/Feb/2013

-   Updates
    -   Added DNN7 version compiled against DNN 7.0.0

```Minimum configuration DNN 5.2.3+ / DNN 6+ / .NET 3.5```

## 02.01.05

13/Jul/2012

-   Enhancements
    -   Minimum supported install is now DNN 5.2.3 and DNN 6.0.0, both
        have to be .NET Framework 3.5 minimum
    -   Updated to support Azure deployment
    -   Updated settings layouts to take advantage of DNN6 styling and
        better conform to DNN6 standard

## 02.01.03

06/Mar/2012

-   Fixes
    -   Fixed a bug with import/export introduced in 2.1.2

## 02.01.02

Mar/3/2012

-   Updated install for DNN5/6
-   Updated settings styling for DNN6

## 02.01.00

-   Fixed DNN6 compatibility issues
-   Minimum DNN version required is now 5.1, or DNN 6
-   Last DNN4 version of this module is now 02.00.01

## 02.00.01

-   Fixed a bug that required two clicks of a KeepHidden link before the
    module was finally hidden
-   Last DNN4 version of this module

## 02.00.00

-   Removed 2000 character limit from text message
-   Added standard tabbed interface to settings page
-   Removed old TokenReplace
-   Added standard token replacement based on DNNStuff.Utilities.dll
-   Added text to show when module is hidden (optional)
    -   If this text is specified, instead of the module hiding itself
        it will display this instead

-   Minimum DNN version supported is now 4.6.2
-   Added [UNHIDE] token
    -   When used with the 'show text when hidden' the user can click an
        unhide link to show content again

-   Added session expiring view counts
    -   Helpful if you want to show a module for a number of views
        before expiring but have it show again when the user comes back
        the next time

## 01.01.02

-   Hides the module for admins and editors when in view mode if they
    have hidden the module

## 01.01.01

-   Changed Keep Hidden option to choice of option button or link
-   Added [KeepHiddenLink] token
    -   equivalent to combination of KeepHidden checked and Hide link

-   Added WelcomeHide classname to the Hide link button for easier
    styling
-   Added WelcomeKeepHidden classname to the Keep Hidden option button
    and link for easier styling

## 01.01.00

-   Added token help
-   Added content version
-   Added keep hidden checkbox and keep hidden message
-   Added close button and close message
-   Added standard DNN token replacement
-   Added custom token replacements
    -   [Views] - \# of views so far
    -   [ViewsRemaining] - \# of views before module hides itself


