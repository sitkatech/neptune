create function dbo.fGetTGUInputGeometry (
	@ObjectIDs as varchar(max), --comma-separated list of ids
	@ObjectType as varchar(max)
)
returns Geometry
as
BEGIN

Declare @ReturnGeometry geometry;


IF @ObjectType = 'Delineation'
	Select @ReturnGeometry = geometry::UnionAggregate(DelineationGeometry) from dbo.Delineation where DelineationID in (select * from STRING_SPLIT(@ObjectIDs, ','));
ELSE IF @ObjectType = 'OnlandVisualTrashAssessmentArea'
	Select @ReturnGeometry = geometry::UnionAggregate(OnlandVisualTrashAssessmentAreaGeometry) from dbo.OnlandVisualTrashAssessmentArea where OnlandVisualTrashAssessmentAreaID in (select * from STRING_SPLIT(@ObjectIDs, ','));
ELSE
	return cast('Invalid Object Type for fGetTGUInputGeometry' as geometry);


RETURN @ReturnGeometry
END
