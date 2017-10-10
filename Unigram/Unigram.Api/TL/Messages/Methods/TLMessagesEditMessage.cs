// <auto-generated/>
using System;

namespace Telegram.Api.TL.Messages.Methods
{
	/// <summary>
	/// RCP method messages.editMessage.
	/// Returns <see cref="Telegram.Api.TL.TLUpdatesBase"/>
	/// </summary>
	public partial class TLMessagesEditMessage : TLObject
	{
		[Flags]
		public enum Flag : Int32
		{
			NoWebPage = (1 << 1),
			Stop = (1 << 12),
			Message = (1 << 11),
			ReplyMarkup = (1 << 2),
			Entities = (1 << 3),
			GeoPoint = (1 << 13),
		}

		public bool IsNoWebPage { get { return Flags.HasFlag(Flag.NoWebPage); } set { Flags = value ? (Flags | Flag.NoWebPage) : (Flags & ~Flag.NoWebPage); } }
		public bool IsStop { get { return Flags.HasFlag(Flag.Stop); } set { Flags = value ? (Flags | Flag.Stop) : (Flags & ~Flag.Stop); } }
		public bool HasMessage { get { return Flags.HasFlag(Flag.Message); } set { Flags = value ? (Flags | Flag.Message) : (Flags & ~Flag.Message); } }
		public bool HasReplyMarkup { get { return Flags.HasFlag(Flag.ReplyMarkup); } set { Flags = value ? (Flags | Flag.ReplyMarkup) : (Flags & ~Flag.ReplyMarkup); } }
		public bool HasEntities { get { return Flags.HasFlag(Flag.Entities); } set { Flags = value ? (Flags | Flag.Entities) : (Flags & ~Flag.Entities); } }
		public bool HasGeoPoint { get { return Flags.HasFlag(Flag.GeoPoint); } set { Flags = value ? (Flags | Flag.GeoPoint) : (Flags & ~Flag.GeoPoint); } }

		public Flag Flags { get; set; }
		public TLInputPeerBase Peer { get; set; }
		public Int32 Id { get; set; }
		public String Message { get; set; }
		public TLReplyMarkupBase ReplyMarkup { get; set; }
		public TLVector<TLMessageEntityBase> Entities { get; set; }
		public TLInputGeoPointBase GeoPoint { get; set; }

		public TLMessagesEditMessage() { }
		public TLMessagesEditMessage(TLBinaryReader from)
		{
			Read(from);
		}

		public override TLType TypeId { get { return TLType.MessagesEditMessage; } }

		public override void Read(TLBinaryReader from)
		{
			Flags = (Flag)from.ReadInt32();
			Peer = TLFactory.Read<TLInputPeerBase>(from);
			Id = from.ReadInt32();
			if (HasMessage) Message = from.ReadString();
			if (HasReplyMarkup) ReplyMarkup = TLFactory.Read<TLReplyMarkupBase>(from);
			if (HasEntities) Entities = TLFactory.Read<TLVector<TLMessageEntityBase>>(from);
			if (HasGeoPoint) GeoPoint = TLFactory.Read<TLInputGeoPointBase>(from);
		}

		public override void Write(TLBinaryWriter to)
		{
			UpdateFlags();

			to.Write(0x5D1B8DD);
			to.Write((Int32)Flags);
			to.WriteObject(Peer);
			to.Write(Id);
			if (HasMessage) to.Write(Message);
			if (HasReplyMarkup) to.WriteObject(ReplyMarkup);
			if (HasEntities) to.WriteObject(Entities);
			if (HasGeoPoint) to.WriteObject(GeoPoint);
		}

		private void UpdateFlags()
		{
			HasMessage = Message != null;
			HasReplyMarkup = ReplyMarkup != null;
			HasEntities = Entities != null;
			HasGeoPoint = GeoPoint != null;
		}
	}
}