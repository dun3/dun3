// <source lang="javascript">

/*
  Helper routines to create "quick modify commands" used by [[MediaWiki:QuickMod.js]]. For usage
  info see e.g. the tests at [[User:Lupo/qmt.js]].

  Author: [[User:Lupo]], January 2009
  License: Quadruple licensed GFDL, GPL, LGPL and Creative Commons Attribution 3.0 (CC-BY-3.0)
 
  Choose whichever license of these you like best :-)
 */

var QuickModify =
{
  HISTORY          : 'h',
    // Perform a revision ID check. Stop processing commands if the current revision ID of the page
    // being edited != the argument. If so, a warning is added to the edit page, with a link to run
    // the commands all the same. Argument: int or string. If the argument is empty (i.e., the empty
    // string), no revision ID check will be done. If the HISTORY action is used, it should be the
    // first action on a page. See [[MediaWiki:Gadget-GalleryDetails.js]] for an application of this.
  APPEND           : 'a',
    // Add to the end of the text. Argument: string
  INSERT           : 'i',
    // Insert in front of text. Argument: string
  EDIT_COMMENT_SET : 'e',
    // Set the edit comment. Argument: string
  EDIT_COMMENT_APPEND : 'E',
    // Append text to the edit comment. Argument: string
  DEL              : 'd',
    // Delete a specified pattern. Argument: either a string or a regular expression
  REPLACE          : 'r',
    // Replace a specified pattern. Arguments: first arg as DEL, second arg replacement string
  SAVE             : 's',
    // Save the page. Optional argument: edit summary (string). Should be the last action.
  SAVE_AND_CLOSE   : 'c',
    // Save the page and close the tab. Optional argument: edit summary (string). Should be the last
    // action.
  
  actions : function ()
  {
    // Parameters: sequence of arrays, each describing one action. Null arguments are ignored.
    var args = arguments;
    var result = "";
    for (var i = 0; i < args.length; i++) {
      if (args[i] != null) {
        var action = "";
        if (args[i][0] == 'd' || args[i][0] == 'r') {
          // Cannot simply join args[i] because we need to mark string/regexp args to replace/delete
          action = args[i][0] + '\x01';
          if (typeof (args[i][1]) == 'string')
            action += 's' + args[i][1];
          else
            action += 'r' + args[i][1].toString ();
          if (args[i][0] == 'r') action += '\x01' + args[i][2];
        } else {
          action = args[i].join ('\x01');
        }
        if (action.length > 0) result += (result.length > 0 ? '\x02' : "") + action;
      }
    }
    return result;
  },
  
  join : function (page, actions)
  {
    return '\x04' + page + '\x03' + actions;
  },
  
  execute : function (edit_lk, commands, same_window)
  {
    edit_lk = edit_lk
            + '&withJS=MediaWiki:QuickMod.js'
            + '&qmcmd=' + encodeURIComponent (commands);
    if (!same_window)
      window.open (edit_lk , '_blank');
    else
      window.location = edit_lk;
  }
  
}

// </source>