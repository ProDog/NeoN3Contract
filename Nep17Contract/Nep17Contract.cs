﻿//using Neo;
//using Neo.SmartContract;
//using Neo.SmartContract.Framework;
//using Neo.SmartContract.Framework.Native;
//using Neo.SmartContract.Framework.Services;
//using System.ComponentModel;
//using System.Numerics;

//namespace Neo3Contract
//{
//    [DisplayName("MyTestContract")]
//    [ManifestExtra("Author", "ProDog")]
//    [ManifestExtra("Email", "xxxxxx")]
//    [ManifestExtra("Description", "This is a Neo3 contract example")]
//    [SupportedStandards("NEP", "null")]
//    [ContractPermission("*", "*")]
//    public partial class Nep17Contract : Nep17Token
//    {
//        [InitialValue("NUNtEBBbJkmPrmhiVSPN6JuM7AcE8FJ5sE", ContractParameterType.Hash160)]
//        public static UInt160 Owner;

//        [InitialValue("f563ea40bc283d4d0e05c48ea305b3f2a07340ef", ContractParameterType.ByteArray)]
//        public static UInt160 ScriptHash;

//        public delegate void Notify(params object[] arg);

//        [DisplayName("test_event")]
//        public static event Notify OnNotify;

//        protected const byte Prefix_Owner = 0x10;

//        public static void _deploy(object data, bool update)
//        {
//            if (update)
//            {
//                OnNotify("update", 1);
//            }
//            else
//            {
//                Mint(Owner, 1_0000_0000_00000000);                
//                Storage.Put(Storage.CurrentContext, new byte[] { Prefix_Owner }, Owner);
//                OnNotify("deploy", 1);
//            }
//        }

//        public static void _test(object data, bool update)
//        {
//            if (update)
//            {
//                OnNotify("update", 1);
//            }
//            else
//            {
//                Mint(Owner, 1_0000_0000_00000000);
//                Storage.Put(Storage.CurrentContext, new byte[] { Prefix_Owner }, Owner);
//                OnNotify("deploy", 1);
//            }
//        }

//        public override string Symbol()
//        {
//            return "Neo-Nep17";
//        }

//        public override byte Decimals()
//        {
//            return 8;
//        }

//        public static bool Verify()
//        {
//            return Runtime.CheckWitness(GetOwner());
//        }

//        public static bool IsOwner()
//        {
//            return Runtime.CheckWitness(GetOwner());
//        }

//        public static UInt160 GetOwner()
//        {
//            return (UInt160)Storage.Get(Storage.CurrentContext, new byte[] { Prefix_Owner });
//        }

//        public static bool SetOwner(UInt160 newOwner)
//        {
//            if (!IsOwner()) return false;
//            Storage.Put(Storage.CurrentContext, new byte[] { Prefix_Owner }, newOwner);
//            return true;
//        }

//        public static bool Mint(UInt160 to, BigInteger amt)
//        {
//            if (!IsOwner()) return false;
//            Nep17Token.Mint(to, amt);
//            return true;
//        }

//        public static bool Burn(UInt160 account, BigInteger amt)
//        {
//            if (!IsOwner()) return false;
//            Nep17Token.Burn(account, amt);
//            return true;
//        }

//        public static bool Update(ByteString nef, string manifest)
//        {
//            if (!IsOwner()) return false;
//            ContractManagement.Update(nef, manifest, null);
//            return true;
//        }

//        public static bool Destroy()
//        {
//            if (!IsOwner()) return false;
//            ContractManagement.Destroy();
//            return true;
//        }

//        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
//        {
//            OnNotify("OnNEP17Payment", from, amount, data);
//        }
//    }
//}
