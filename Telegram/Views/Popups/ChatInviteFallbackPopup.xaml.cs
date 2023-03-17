﻿using System.Collections.Generic;
using System.Linq;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Controls.Cells;
using Telegram.Services;
using Telegram.Td.Api;
using Windows.UI.Xaml.Controls;

namespace Telegram.Views.Popups
{
    public sealed partial class ChatInviteFallbackPopup : ContentPopup
    {
        private readonly IClientService _clientService;
        private readonly ChatInviteLink _inviteLink;

        public ChatInviteFallbackPopup(IClientService clientService, long chatId, IList<long> userIds)
        {
            InitializeComponent();

            var chat = clientService.GetChat(chatId);
            var users = clientService.GetUsers(userIds);

            _clientService = clientService;
            _inviteLink = GetInviteLink(chat);

            string title;
            string message;
            string primary;
            string secondary;

            if (_inviteLink != null)
            {
                title = Strings.Resources.ChannelInviteViaLink;
                message = users.Count == 1
                    ? string.Format(Strings.Resources.InviteChannelRestrictedUsersOne, users[0].FullName())
                    : Locale.Declension("InviteChannelRestrictedUsers", users.Count);

                primary = Strings.Resources.SendInviteLink;
                secondary = Strings.Resources.ActionSkip;
            }
            else
            {
                title = Strings.Resources.ChannelInviteViaLinkRestricted;
                message = users.Count == 1
                    ? string.Format(Strings.Resources.InviteChannelRestrictedUsers2One, users[0].FullName())
                    : Locale.Declension("InviteChannelRestrictedUsers2", users.Count);

                primary = Strings.Resources.Close;
                secondary = null;
            }

            Title = title;
            ScrollingHost.ItemsSource = users;
            ScrollingHost.SelectionMode = _inviteLink != null
                ? ListViewSelectionMode.Multiple
                : ListViewSelectionMode.None;
            ScrollingHost.SelectAll();

            TextBlockHelper.SetMarkdown(MessageLabel, message);

            PrimaryButtonText = primary;
            SecondaryButtonText = secondary;
        }

        private ChatInviteLink GetInviteLink(Chat chat)
        {
            if (_clientService.TryGetSupergroupFull(chat, out var supergroup))
            {
                return supergroup.InviteLink;
            }
            else if (_clientService.TryGetBasicGroupFull(chat, out var basicGroup))
            {
                return basicGroup.InviteLink;
            }

            return null;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (_inviteLink != null)
            {
                foreach (var user in ScrollingHost.SelectedItems.OfType<User>())
                {
                    var chat = await _clientService.SendAsync(new CreatePrivateChat(user.Id, true)) as Chat;
                    if (chat != null)
                    {
                        _clientService.Send(new SendMessage(chat.Id, 0, 0, null, null, new InputMessageText(new FormattedText(_inviteLink.InviteLink, new TextEntity[0]), false, false)));
                    }
                }
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = ScrollingHost.SelectionMode == ListViewSelectionMode.Multiple
                && ScrollingHost.SelectedItems.Count > 0;
        }

        #region Recycle

        private void OnChoosingItemContainer(ListViewBase sender, ChoosingItemContainerEventArgs args)
        {
            if (args.ItemContainer == null)
            {
                args.ItemContainer = new MultipleListViewItem(false);
                args.ItemContainer.Style = sender.ItemContainerStyle;
                args.ItemContainer.ContentTemplate = sender.ItemTemplate;
            }

            args.IsContainerPrepared = true;
        }

        private void OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                return;
            }
            else if (args.ItemContainer.ContentTemplateRoot is ChatShareCell content)
            {
                content.UpdateState(args.ItemContainer.IsSelected, false);
                content.UpdateUser(_clientService, args, OnContainerContentChanging);
            }
        }

        #endregion
    }
}
