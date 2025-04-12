using Mutagen.Bethesda.Plugins.Aspects;
using Mutagen.Bethesda.Strings;

namespace FormPicker.Utils
{
    public static class RecordUtil
    {
/*        private static readonly Dictionary<Type, string> RecordTypes = new()
        {
            { typeof(IAcousticSpaceGetter), "AcousticSpace" },
            { typeof(IActionRecordGetter), "ActionRecord" },
            { typeof(IActivatorGetter), "Activator" },
            { typeof(IActorValueInformationGetter), "ActorValueInformation" },
            { typeof(IAddonNodeGetter), "AddonNode" },
            { typeof(IAlchemicalApparatusGetter), "AlchemicalApparatus" },
            { typeof(IAmmunitionGetter), "Ammunition" },
            { typeof(IAnimatedObjectGetter), "AnimatedObject" },
            { typeof(IArmorGetter), "Armor" },
            { typeof(IArmorAddonGetter), "ArmorAddon" },
            { typeof(IArtObjectGetter), "ArtObject" },
            { typeof(IAssociationTypeGetter), "AssociationType" },
            { typeof(IBodyPartDataGetter), "BodyPartData" },
            { typeof(IBookGetter), "Book" },
            { typeof(ICameraPathGetter), "CameraPath" },
            { typeof(ICameraShotGetter), "CameraShot" },
            { typeof(ICellGetter), "Cell" },
            { typeof(IClassGetter), "Class" },
            { typeof(IClimateGetter), "Climate" },
            { typeof(ICollisionLayerGetter), "CollisionLayer" },
            { typeof(IColorRecordGetter), "ColorRecord" },
            { typeof(ICombatStyleGetter), "CombatStyle" },
            { typeof(IConstructibleObjectGetter), "ConstructibleObject" },
            { typeof(IContainerGetter), "Container" },
            { typeof(IDebrisGetter), "Debris" },
            { typeof(IDefaultObjectManagerGetter), "DefaultObjectManager" },
            { typeof(IDoorGetter), "Door" },
            { typeof(IDualCastDataGetter), "DualCastData" },
            { typeof(IEffectShaderGetter), "EffectShader" },
            { typeof(IEncounterZoneGetter), "EncounterZone" },
            { typeof(IEquipTypeGetter), "EquipType" },
            { typeof(IExplosionGetter), "Explosion" },
            { typeof(IEyesGetter), "Eyes" },
            { typeof(IFactionGetter), "Faction" },
            { typeof(IFloraGetter), "Flora" },
            { typeof(IFootstepGetter), "Footstep" },
            { typeof(IFootstepSetGetter), "FootstepSet" },
            { typeof(IFormListGetter), "FormList" },
            { typeof(IFurnitureGetter), "Furniture" },
            { typeof(IGameSettingGetter), "GameSetting" },
            { typeof(IGlobalGetter), "Global" },
            { typeof(IGrassGetter), "Grass" },
            { typeof(IHairGetter), "Hair" },
            { typeof(IHazardGetter), "Hazard" },
            { typeof(IHeadPartGetter), "HeadPart" },
            { typeof(IIdleAnimationGetter), "IdleAnimation" },
            { typeof(IIdleMarkerGetter), "IdleMarker" },
            { typeof(IImageSpaceGetter), "ImageSpace" },
            { typeof(IImageSpaceAdapterGetter), "ImageSpaceAdapter" },
            { typeof(IImpactGetter), "Impact" },
            { typeof(IImpactDataSetGetter), "ImpactDataSet" },
            { typeof(IIngredientGetter), "Ingredient" },
            { typeof(IKeyGetter), "Key" },
            { typeof(IKeywordGetter), "Keyword" },
            { typeof(ILensFlareGetter), "LensFlare" },
            { typeof(ILeveledItemGetter), "LeveledItem" },
            { typeof(ILeveledNpcGetter), "LeveledNpc" },
            { typeof(ILeveledSpellGetter), "LeveledSpell" },
            { typeof(ILightGetter), "Light" },
            { typeof(ILightingTemplateGetter), "LightingTemplate" },
            { typeof(ILoadScreenGetter), "LoadScreen" },
            { typeof(ILocationGetter), "Location" },
            { typeof(IMagicEffectGetter), "MagicEffect" },
            { typeof(IMaterialObjectGetter), "MaterialObject" },
            { typeof(IMaterialTypeGetter), "MaterialType" },
            { typeof(IMessageGetter), "Message" },
            { typeof(IMiscItemGetter), "MiscItem" },
            { typeof(IMovementTypeGetter), "MovementType" },
            { typeof(IMusicTrackGetter), "MusicTrack" },
            { typeof(IMusicTypeGetter), "MusicType" },
            { typeof(INavigationMeshGetter), "NavigationMesh" },
            { typeof(INavigationMeshInfoMapGetter), "NavigationMeshInfoMap" },
            { typeof(INpcGetter), "Npc" },
            { typeof(IObjectEffectGetter), "ObjectEffect" },
            { typeof(IOutfitGetter), "Outfit" },
            { typeof(IPackageGetter), "Package" },
            { typeof(IPerkGetter), "Perk" },
            { typeof(IPlacedNpcGetter), "PlacedNpc" },
            { typeof(IPlacedObjectGetter), "PlacedObject" },
            { typeof(IPlacedTrapGetter), "PlacedTrap" },
            { typeof(IProjectileGetter), "Projectile" },
            { typeof(IQuestGetter), "Quest" },
            { typeof(IRaceGetter), "Race" },
            { typeof(IRegionGetter), "Region" },
            { typeof(IRelationshipGetter), "Relationship" },
            { typeof(IReverbParametersGetter), "ReverbParameters" },
            { typeof(ISceneGetter), "Scene" },
            { typeof(IScrollGetter), "Scroll" },
            { typeof(IShaderParticleGeometryGetter), "ShaderParticleGeometry" },
            { typeof(IShoutGetter), "Shout" },
            { typeof(ISoulGemGetter), "SoulGem" },
            { typeof(ISoundCategoryGetter), "SoundCategory" },
            { typeof(ISoundDescriptorGetter), "SoundDescriptor" },
            { typeof(ISoundMarkerGetter), "SoundMarker" },
            { typeof(ISoundOutputModelGetter), "SoundOutputModel" },
            { typeof(ISpellGetter), "Spell" },
            { typeof(IStaticGetter), "Static" },
            { typeof(ITalkingActivatorGetter), "TalkingActivator" },
            { typeof(ITextureSetGetter), "TextureSet" },
            { typeof(ITreeGetter), "Tree" },
            { typeof(IVisualEffectGetter), "VisualEffect" },
            { typeof(IVoiceTypeGetter), "VoiceType" },
            { typeof(IVolumetricLightingGetter), "VolumetricLighting" },
            { typeof(IWaterGetter), "Water" },
            { typeof(IWeaponGetter), "Weapon" },
            { typeof(IWeatherGetter), "Weather" },
            { typeof(IWordOfPowerGetter), "WordOfPower" },
            { typeof(IWorldspaceGetter), "Worldspace" }
        };*/

        /*        public static string GetType<T>(T record) where T : IMajorRecordGetter
                {
                    if (RecordTypes.ContainsKey(record.Type))
                    {
                        return RecordTypes[record.Type];
                    }
                    else return record.Type.Name;
                }*/

        public static string? GetName<T>(T record) where T : ITranslatedNamedRequiredGetter
        {
            if (record.Name.TryLookup(Language.Russian, out var name))
            {
                return name;
            }
            else
            {
                return StringUtil.Enc1252ToUTF8(record.Name.String);
            }
        }
    }
}
