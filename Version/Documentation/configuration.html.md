# Welcome Configuration 

## Common Tab

### Text to Display

In this field you enter any text you wish to display to the user. The
text can include a number of [Welcome\_Tokens|predefined tokens], in
addition to the standard DNN replacement tokens.

### Page Views before auto hide

This defines the number of page views before the module is hidden from
view. If you specify a value of 0 (zero) here, the module will only be
hidden if a hide button is present and the user clicks it.

### Reset View Count after Session Ends

View counts are normally stored in a browser cookie that doesn't expire.
If this option is selected, the view count is stored in a session cookie
and the views will be reset after the user closes their browser so when
they return they'll see the content once again until the view count max
is hit again.

This option is useful if you want to provide a brief welcome message
each time the user visits the site and have it auto hide but show again
the next time.

### Show a hide button

Select this option to show a hide button within the text.

### Enter the hide button message

In this field you enter the text you want to display for the hide
button.

### Keep hidden option

Use this option to select whether to show a Keep Hidden checkbox or link
button. If either option is selected, the appropriate control is placed
at the bottom of the content. If you wish to place the control yourself,
embed it in the content using either the [KeepHidden] or the
[KeepHiddenLink] tokens.

### Enter the keep hidden checkbox message

In this field you enter the text you want to display for the keep hidden
checkbox.

### Content Version

The purpose of the content version number is to enable you to reset the
module for new content. This will reverse any prior decisions the user
has made with respect to the module. As an example, lets say you have
some content in the module and you want to change the message. If you
increment this number then the module will behave like you just added it
to the page. If a user had previously viewed the prior version more than
the alloted views or selected 'keep hidden', changing to a new version
will reset the view count and reverse the keep hidden choice back to
square one so that all users start seeing the content again. It's really
intended as a shortcut so you don't have to continually add a new
Welcome module to the page and delete the old one. You could use the
version number to periodically add a new site tip so if a user had
already viewed the previous site tip or hidden it, the new tip will show
again regardless.

![Welcome - Configuration](images\Configuration - Common.png)


## Advanced Tab

### Text to show when hidden

If instead of hiding the module you wish to show some alternate text
when it's hidden, enter the text in here.

This section also supports the [UNHIDE] token which allows your users to
reshow the content.

### Unhide button message

In this field you enter the text you want to display for the unhide
button

![Welcome - Configuration](images\Configuration - Advanced.png)
