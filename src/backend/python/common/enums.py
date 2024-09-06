from interfaces import base_enum


class StructuralElement(base_enum.AbstractIdNameEnum):
    """
    Enum для структурных элементов
    """

    JOINT = (1, "Сварной шов")
    BEND = (2, "Изгиб")
    BRANCHING = (3, "Разветвление")
    PATCH = (4, "Заплатка")
