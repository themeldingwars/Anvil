using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ImGuiNET;

namespace Anvil.Aero
{
    public class AeroInspector
    {
        public List<AeroInspectorEntry> Entries = new();
        public object                   Obj;
        public bool                     AllowEdit;

        public void BuildList(object obj)
        {
            Obj = obj;

            var type     = obj.GetType();
            var orderIdx = 0;

            foreach (var f in type.GetFields().Where(f => f.IsPublic)) {
                var entry = new AeroInspectorEntry()
                {
                    Name     = f.Name,
                    EType    = GetEntryTypeFromType(f.FieldType),
                    IsArray  = f.FieldType.IsArray,
                    Ref      = f,
                    OrderIdx = orderIdx++
                };

                Entries.Add(entry);
            }
        }

        public void Draw()
        {
            //if (ImGui.CollapsingHeader("Inspector", ImGuiTreeNodeFlags.DefaultOpen)) {
                if (ImGui.BeginTable("Inspector Table", 2, ImGuiTableFlags.Borders)) {
                    ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.NoSort, 0.2f);
                    ImGui.TableSetupColumn("Value", ImGuiTableColumnFlags.None, 0.4f);
                    ImGui.TableHeadersRow();

                    foreach (var entry in Entries) {
                        DrawEntry(entry);
                    }

                    ImGui.EndTable();
                }
            //}
        }

        private bool DrawEntry(AeroInspectorEntry entry)
        {
            ImGui.TableNextColumn();
            ImGui.Text(entry.Name);
            ImGui.TableNextColumn();

            if (entry.EType == AeroInspectorEntry.EntryType.Unknown) {
                ImGui.Text($"Name: {entry.Name}, EType: {entry.EType}, IsArray: {entry.IsArray}, OrderIdx: {entry.OrderIdx}");

                return false;
            }
            else {
                Func<AeroInspectorEntry, bool> draw = entry.EType switch
                {
                    AeroInspectorEntry.EntryType.Int   => DrawInt,
                    AeroInspectorEntry.EntryType.Long  => DrawLong,
                    AeroInspectorEntry.EntryType.Short => DrawShort,
                    AeroInspectorEntry.EntryType.Uint  => DrawUInt,
                    AeroInspectorEntry.EntryType.Byte  => DrawByte,
                    AeroInspectorEntry.EntryType.Char  => DrawByte,
                    _                                  => DrawUnknown
                };

                ImGui.SetNextItemWidth(200f);
                var hasChanged = draw(entry);
                return hasChanged;
            }
        }

        private bool DrawByte(AeroInspectorEntry entry)
        {
            var val        = (int)entry.GetValue<byte>(Obj);
            var hasChanged = ImGui.InputInt($"###{entry.Name}", ref val);
            if (hasChanged) entry.SetValue(Obj, (byte)val);

            return hasChanged;
        }

        private bool DrawInt(AeroInspectorEntry entry)
        {
            var val        = entry.GetValue<int>(Obj);
            var hasChanged = ImGui.InputInt($"###{entry.Name}", ref val);
            if (hasChanged) entry.SetValue(Obj, val);

            return hasChanged;
        }

        private unsafe bool DrawUInt(AeroInspectorEntry entry)
        {
            var val        = entry.GetValue<uint>(Obj);
            var hasChanged = ImGui.InputScalar($"###{entry.Name}", ImGuiDataType.U32, (IntPtr)(&val));
            if (hasChanged) entry.SetValue(Obj, val);

            return hasChanged;
        }

        private unsafe bool DrawLong(AeroInspectorEntry entry)
        {
            var val        = entry.GetValue<long>(Obj);
            var hasChanged = ImGui.InputScalar($"###{entry.Name}", ImGuiDataType.S64, (IntPtr)(&val));
            if (hasChanged) entry.SetValue(Obj, val);

            return hasChanged;
        }

        private bool DrawShort(AeroInspectorEntry entry)
        {
            var val        = (int)entry.GetValue<short>(Obj);
            var hasChanged = ImGui.InputInt($"###{entry.Name}", ref val);
            if (hasChanged) entry.SetValue<short>(Obj, (short)val);

            return hasChanged;
        }

        private bool DrawUnknown(AeroInspectorEntry entry)
        {
            ImGui.Text($"{entry.Ref.GetValue(Obj)}");

            return false;
        }

        private AeroInspectorEntry.EntryType GetEntryTypeFromType(Type type)
        {
            var eType = Type.GetTypeCode(type) switch
            {
                TypeCode.Int32  => AeroInspectorEntry.EntryType.Int,
                TypeCode.Int64  => AeroInspectorEntry.EntryType.Long,
                TypeCode.Int16  => AeroInspectorEntry.EntryType.Short,
                TypeCode.UInt32 => AeroInspectorEntry.EntryType.Uint,
                TypeCode.UInt64 => AeroInspectorEntry.EntryType.Ulong,
                TypeCode.UInt16 => AeroInspectorEntry.EntryType.Ushort,
                TypeCode.Byte   => AeroInspectorEntry.EntryType.Byte,
                TypeCode.Char   => AeroInspectorEntry.EntryType.Char,
                TypeCode.Single => AeroInspectorEntry.EntryType.Float,
                TypeCode.Double => AeroInspectorEntry.EntryType.Double,
                TypeCode.String => AeroInspectorEntry.EntryType.String,
                _               => AeroInspectorEntry.EntryType.Unknown
            };

            return eType;
        }
    }

    public class AeroInspectorEntry
    {
        public string    Name;
        public EntryType EType;
        public bool      IsArray;
        public FieldInfo Ref;
        public int       ColorIdx;
        public int       Offset;
        public int       Size;
        public int       OrderIdx;

        public T GetValue<T>(object obj)
        {
            var val = (T)Ref.GetValue(obj);
            return val;
        }

        public void SetValue<T>(object obj, T val)
        {
            Ref.SetValue(obj, val);
        }

        public enum EntryType
        {
            Byte,
            Char,
            Int,
            Uint,
            Long,
            Ulong,
            Short,
            Ushort,
            Float,
            Double,
            String,
            Vector2,
            Vector3,
            Vector4,
            Quaternion,
            AeroBlock,
            Unknown
        }
    }
}